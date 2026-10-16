using System;
using System.Collections.Generic;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Services
{
    /// <summary>
    /// Records what a resident searches for and which events they open, and builds
    /// an InterestProfile from that activity for the recommendation engine.
    ///
    ///   Queue&lt;SearchRecord&gt;   the last 10 searches with what they found; oldest leaves first (FIFO)
    ///   Queue&lt;LocalEvent&gt;     the last 10 events the resident opened
    ///   HashSet&lt;int&gt;          every event opened this session, so it is not suggested again
    ///   HashSet&lt;string&gt;       common words ignored when counting keywords
    ///
    /// Because only the last ten searches and views are kept, and newer ones count for
    /// more when the profile is built, suggestions follow what the resident is looking
    /// at now while still remembering what they looked at a little earlier.
    /// </summary>
    public class SearchTracker
    {
        public const int MaxRecentSearches = 10;
        public const int MaxRecentViews = 10;

        // How much each kind of activity adds to the profile (before the recency factor).
        private const double CategoryFilterWeight = 1.0;
        private const double KeywordWeight = 1.0;
        private const double ResultsWeight = 1.0;        // shared across the events a search found
        private const double ViewCategoryWeight = 1.5;   // opening an event shows stronger interest
        private const double ViewTagWeight = 1.0;
        private const double DayWeight = 1.0;

        // Only this many results from each search are kept for the profile.
        private const int MaxResultsPerRecord = 20;

        private const int MinimumTermLength = 3;

        /// <summary>
        /// The tracker used by the application. It lives for the whole session, so
        /// suggestions keep their history after the events page is closed and reopened.
        /// </summary>
        public static SearchTracker Shared { get; } = new SearchTracker();

        private static readonly HashSet<string> StopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "the", "and", "for", "with", "from", "near", "event", "events", "day", "days", "what", "when", "where"
        };

        private readonly Queue<SearchRecord> _recentSearches = new Queue<SearchRecord>();
        private readonly Queue<LocalEvent> _recentViews = new Queue<LocalEvent>();
        private readonly HashSet<int> _viewedEventIds = new HashSet<int>();

        /// <summary>
        /// Number of searches recorded this session, including ones that have since
        /// left the recent-searches queue.
        /// </summary>
        public int SearchCount { get; private set; }

        public int ViewCount
        {
            get { return _viewedEventIds.Count; }
        }

        public bool HasHistory
        {
            get { return _recentSearches.Count > 0 || _recentViews.Count > 0; }
        }

        public int RecentSearchCount
        {
            get { return _recentSearches.Count; }
        }

        /// <summary>
        /// The searches currently in the queue, oldest first.
        /// </summary>
        public List<SearchQuery> GetRecentQueries()
        {
            List<SearchQuery> queries = new List<SearchQuery>(_recentSearches.Count);
            foreach (SearchRecord record in _recentSearches)
            {
                queries.Add(record.Query);
            }
            return queries;
        }

        public bool HasViewed(int eventId)
        {
            return _viewedEventIds.Contains(eventId);
        }

        // ------------------------------------------------------------------
        // Recording
        // ------------------------------------------------------------------

        /// <summary>
        /// Records a search and what it found. Searches with no filters say nothing
        /// about interests, so they are ignored.
        /// </summary>
        public void RecordSearch(SearchQuery query, EventSearchResult? result)
        {
            if (query == null || query.IsEmpty) return;

            // Results only describe interests for keyword or category searches; a
            // date-only search returns everything on those days.
            List<LocalEvent> found = new List<LocalEvent>();
            if ((query.HasKeyword || query.HasCategory) && result != null)
            {
                foreach (LocalEvent localEvent in result.ToList())
                {
                    found.Add(localEvent);
                    if (found.Count == MaxResultsPerRecord) break;
                }
            }

            // Queue: the newest search joins the back; once there are more than ten,
            // the oldest is removed from the front.
            _recentSearches.Enqueue(new SearchRecord(query, found));
            while (_recentSearches.Count > MaxRecentSearches)
            {
                _recentSearches.Dequeue();
            }

            SearchCount++;
        }

        /// <summary>
        /// Records that the resident opened an event's details. Each event only counts
        /// once, so opening the same card repeatedly does not skew the suggestions.
        /// </summary>
        public void RecordView(LocalEvent localEvent)
        {
            if (localEvent == null) return;

            // HashSet.Add returns false when the ID is already in the set.
            if (!_viewedEventIds.Add(localEvent.Id)) return;

            _recentViews.Enqueue(localEvent);
            while (_recentViews.Count > MaxRecentViews)
            {
                _recentViews.Dequeue();
            }
        }

        public void Clear()
        {
            _recentSearches.Clear();
            _recentViews.Clear();
            _viewedEventIds.Clear();
            SearchCount = 0;
        }

        // ------------------------------------------------------------------
        // Building the interest profile
        // ------------------------------------------------------------------

        /// <summary>
        /// Builds an interest profile from the queued searches and views. Items are
        /// weighted by how recent they are: in a queue of n items the newest counts
        /// fully (n/n) and the oldest counts 1/n.
        /// </summary>
        public InterestProfile BuildProfile()
        {
            InterestProfile profile = new InterestProfile();

            int position = 0;
            int searchTotal = _recentSearches.Count;
            foreach (SearchRecord record in _recentSearches)
            {
                position++;
                AddSearchToProfile(profile, record, position / (double)searchTotal);
            }

            position = 0;
            int viewTotal = _recentViews.Count;
            foreach (LocalEvent viewed in _recentViews)
            {
                position++;
                double recency = position / (double)viewTotal;

                profile.AddCategory(viewed.Category, ViewCategoryWeight * recency, true);
                profile.AddDay(viewed.StartDateTime.DayOfWeek, DayWeight * recency);

                foreach (string tag in viewed.Tags)
                {
                    profile.AddTag(tag, ViewTagWeight * recency);
                }
            }

            return profile;
        }

        private static void AddSearchToProfile(InterestProfile profile, SearchRecord record, double recency)
        {
            SearchQuery query = record.Query;

            if (query.Category != null)
            {
                profile.AddCategory(query.Category, CategoryFilterWeight * recency, true);
            }

            foreach (string term in GetMeaningfulTerms(query.Keyword))
            {
                profile.AddKeyword(term, KeywordWeight * recency);
            }

            // What the search found hints at related categories and topics. The weight
            // is shared out across the results, so a broad search counts no more than
            // a narrow one.
            if (record.Results.Count > 0)
            {
                double share = ResultsWeight * recency / record.Results.Count;

                foreach (LocalEvent found in record.Results)
                {
                    profile.AddCategory(found.Category, share, false);

                    foreach (string tag in found.Tags)
                    {
                        profile.AddTag(tag, share);
                    }
                }
            }

            // A search for one specific day (for example from the calendar) says
            // something about which days of the week suit the resident.
            if (query.FromDate.HasValue && query.FromDate == query.ToDate)
            {
                profile.AddDay(query.FromDate.Value.DayOfWeek, DayWeight * recency);
            }
        }

        /// <summary>
        /// Splits a keyword into words, dropping very short words and common words
        /// such as "the" that would match almost every event.
        /// </summary>
        public static List<string> GetMeaningfulTerms(string? keyword)
        {
            List<string> terms = new List<string>();
            if (string.IsNullOrWhiteSpace(keyword)) return terms;

            // A set removes repeats, so "water water" only counts once.
            HashSet<string> seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            string[] parts = keyword.Split(new[] { ' ', ',' },
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (string part in parts)
            {
                if (part.Length < MinimumTermLength) continue;
                if (StopWords.Contains(part)) continue;

                if (seen.Add(part))
                {
                    terms.Add(part.ToLowerInvariant());
                }
            }

            return terms;
        }

        /// <summary>
        /// One entry in the recent-searches queue: the search and the events it found.
        /// </summary>
        private sealed class SearchRecord
        {
            public SearchQuery Query { get; }
            public IReadOnlyList<LocalEvent> Results { get; }

            public SearchRecord(SearchQuery query, List<LocalEvent> results)
            {
                Query = query;
                Results = results;
            }
        }
    }
}
