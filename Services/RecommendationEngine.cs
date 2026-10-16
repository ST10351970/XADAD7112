using System;
using System.Collections.Generic;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Services
{
    /// <summary>
    /// Suggests events the resident is likely to be interested in, using the
    /// InterestProfile that SearchTracker builds from their recent activity.
    ///
    /// Algorithm (weighted content-based scoring with top-k selection):
    ///   1. Candidates are upcoming events the resident has not opened and that are
    ///      not already on screen in the current results.
    ///   2. Each candidate is scored on four signals, each scaled to 0..1:
    ///        category   weight of the event's category relative to the strongest one   x 3.0
    ///        keywords   share of the resident's searched words that match the event     x 2.0
    ///        topics     tags shared with the profile (a set intersection)               x 2.0
    ///        day        falls on a day of the week they often look at                   x 0.5
    ///      plus a small bonus for events happening soon, which mostly breaks ties.
    ///      Events with too little in common with the profile are left out.
    ///   3. The best k are kept with a min-heap (PriorityQueue) of size k: each scored
    ///      event is added and, once there are more than k, the lowest is removed.
    ///      This costs O(n log k) instead of sorting every candidate.
    ///   4. If fewer than k events qualify, the list is topped up with the soonest
    ///      events, labelled as such.
    /// </summary>
    public class RecommendationEngine
    {
        private const double CategoryFactor = 3.0;
        private const double KeywordFactor = 2.0;
        private const double TopicFactor = 2.0;
        private const double DayFactor = 0.5;
        private const double SoonFactor = 0.5;

        // Below this, an event has too little in common with the profile to be
        // called a recommendation.
        private const double MinimumInterest = 0.3;

        // Events further away than this get no "happening soon" bonus.
        private const int SoonHorizonDays = 45;

        // Day-of-week preference is only trusted once there are a few signals.
        private const double MinimumDaySignals = 2.0;

        private readonly EventManager _eventManager;
        private readonly SearchTracker _tracker;

        public RecommendationEngine(EventManager eventManager, SearchTracker tracker)
        {
            _eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
            _tracker = tracker ?? throw new ArgumentNullException(nameof(tracker));
        }

        /// <summary>
        /// Up to "count" suggestions, best first. currentResult is what is on screen
        /// now (may be null); its events are skipped when the search was filtered.
        /// </summary>
        public List<Recommendation> GetRecommendations(EventSearchResult? currentResult, DateTime today, int count)
        {
            List<Recommendation> picks = new List<Recommendation>();
            if (count <= 0) return picks;

            List<LocalEvent> upcoming = _eventManager.GetUpcomingEvents(today, int.MaxValue);

            // When every event is already listed (no filters), skipping the on-screen
            // events would leave nothing to suggest, so only skip them for filtered searches.
            bool skipOnScreen = currentResult != null && !currentResult.Query.IsEmpty;
            List<LocalEvent> candidates = SelectCandidates(upcoming, currentResult, skipOnScreen);

            if (candidates.Count < count && skipOnScreen)
            {
                candidates = SelectCandidates(upcoming, currentResult, false);
            }

            HashSet<int> chosen = new HashSet<int>();

            if (_tracker.HasHistory)
            {
                InterestProfile profile = _tracker.BuildProfile();

                foreach (Recommendation recommendation in PickBest(candidates, profile, today, count))
                {
                    picks.Add(recommendation);
                    chosen.Add(recommendation.Event.Id);
                }
            }

            // Top up with the soonest remaining events. Candidates are already in date order.
            foreach (LocalEvent localEvent in candidates)
            {
                if (picks.Count >= count) break;
                if (chosen.Contains(localEvent.Id)) continue;

                picks.Add(new Recommendation(localEvent, 0, DescribeSoon(localEvent, today), false));
                chosen.Add(localEvent.Id);
            }

            return picks;
        }

        private List<LocalEvent> SelectCandidates(List<LocalEvent> upcoming, EventSearchResult? currentResult,
                                                  bool skipOnScreen)
        {
            List<LocalEvent> candidates = new List<LocalEvent>();

            foreach (LocalEvent localEvent in upcoming)
            {
                if (_tracker.HasViewed(localEvent.Id)) continue;
                if (skipOnScreen && currentResult != null && currentResult.Contains(localEvent.Id)) continue;

                candidates.Add(localEvent);
            }

            return candidates;
        }

        /// <summary>
        /// Keeps the k highest-scoring events using a min-heap of size k and returns
        /// them best first.
        /// </summary>
        private static List<Recommendation> PickBest(List<LocalEvent> candidates, InterestProfile profile,
                                                     DateTime today, int count)
        {
            // PriorityQueue is a min-heap: Dequeue always removes the lowest score,
            // so after each Enqueue the weakest candidate can be dropped cheaply.
            PriorityQueue<Recommendation, double> best = new PriorityQueue<Recommendation, double>();

            foreach (LocalEvent localEvent in candidates)
            {
                Recommendation? scored = Score(localEvent, profile, today);
                if (scored == null) continue;

                best.Enqueue(scored, scored.Score);
                if (best.Count > count)
                {
                    best.Dequeue();
                }
            }

            // The heap hands items back lowest first, so reverse for best first.
            List<Recommendation> ranked = new List<Recommendation>(best.Count);
            while (best.TryDequeue(out Recommendation? recommendation, out _))
            {
                ranked.Add(recommendation);
            }
            ranked.Reverse();

            return ranked;
        }

        /// <summary>
        /// Scores one event against an interest profile. Returns null when the event
        /// has too little in common with it.
        /// </summary>
        public static Recommendation? Score(LocalEvent localEvent, InterestProfile profile, DateTime today)
        {
            if (localEvent == null) throw new ArgumentNullException(nameof(localEvent));
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            // 1. Category, relative to the resident's strongest category.
            double category = 0;
            double maxCategory = profile.MaxCategoryWeight;
            if (maxCategory > 0)
            {
                category = profile.GetCategoryWeight(localEvent.Category) / maxCategory;
            }

            // 2. Keywords: what share of everything they typed matches this event.
            double keywords = 0;
            string? bestTerm = null;
            double bestTermWeight = 0;
            double totalTermWeight = profile.TotalKeywordWeight;

            if (totalTermWeight > 0)
            {
                double matchedWeight = 0;
                foreach (KeyValuePair<string, double> term in profile.KeywordWeights)
                {
                    if (localEvent.MatchesKeyword(term.Key))
                    {
                        matchedWeight += term.Value;
                        if (term.Value > bestTermWeight)
                        {
                            bestTerm = term.Key;
                            bestTermWeight = term.Value;
                        }
                    }
                }
                keywords = matchedWeight / totalTermWeight;
            }

            // 3. Topics: the intersection of this event's tags with the profile's tags.
            //    Shared tags worth twice the strongest tag give full marks.
            HashSet<string> sharedTags = new HashSet<string>(localEvent.Tags, StringComparer.OrdinalIgnoreCase);
            sharedTags.IntersectWith(profile.Tags);

            double topics = 0;
            double maxTag = profile.MaxTagWeight;
            if (maxTag > 0 && sharedTags.Count > 0)
            {
                double sharedWeight = 0;
                foreach (string tag in sharedTags)
                {
                    sharedWeight += profile.GetTagWeight(tag);
                }
                topics = Math.Min(1.0, sharedWeight / (2.0 * maxTag));
            }

            // 4. Day of week, once there are enough signals to mean something.
            double day = 0;
            double maxDay = profile.MaxDayWeight;
            if (maxDay > 0 && profile.TotalDayWeight >= MinimumDaySignals)
            {
                day = profile.GetDayWeight(localEvent.StartDateTime.DayOfWeek) / maxDay;
            }

            double categoryPart = CategoryFactor * category;
            double keywordPart = KeywordFactor * keywords;
            double topicPart = TopicFactor * topics;

            // A matching day alone is too weak to call something a recommendation.
            double contentInterest = categoryPart + keywordPart + topicPart;
            if (contentInterest < MinimumInterest)
            {
                return null;
            }

            int daysAway = Math.Max(0, (localEvent.Date - today.Date).Days);
            double soon = Math.Max(0, 1.0 - (daysAway / (double)SoonHorizonDays));

            double score = contentInterest + (DayFactor * day) + (SoonFactor * soon);

            string reason = ChooseReason(localEvent, profile, categoryPart, keywordPart, topicPart,
                                         bestTerm, sharedTags);

            return new Recommendation(localEvent, score, reason, true);
        }

        // The reason shown on the card comes from whichever signal contributed most.
        private static string ChooseReason(LocalEvent localEvent, InterestProfile profile,
                                           double categoryPart, double keywordPart, double topicPart,
                                           string? bestTerm, HashSet<string> sharedTags)
        {
            double strongest = Math.Max(categoryPart, Math.Max(keywordPart, topicPart));

            if (keywordPart == strongest && bestTerm != null)
            {
                return "Matches your search for \"" + bestTerm + "\"";
            }

            if (topicPart == strongest && sharedTags.Count > 0)
            {
                return "Similar topics: " + DescribeTopTags(sharedTags, profile);
            }

            if (profile.IsExplicitCategory(localEvent.Category))
            {
                return "Based on your interest in " + localEvent.Category;
            }

            return "Related to events you searched for in " + localEvent.Category;
        }

        private static string DescribeTopTags(HashSet<string> sharedTags, InterestProfile profile)
        {
            List<string> tags = new List<string>(sharedTags);
            tags.Sort((a, b) =>
            {
                int byWeight = profile.GetTagWeight(b).CompareTo(profile.GetTagWeight(a));
                return byWeight != 0 ? byWeight : string.Compare(a, b, StringComparison.OrdinalIgnoreCase);
            });

            if (tags.Count > 2)
            {
                tags.RemoveRange(2, tags.Count - 2);
            }

            return string.Join(", ", tags);
        }

        private static string DescribeSoon(LocalEvent localEvent, DateTime today)
        {
            int daysAway = (localEvent.Date - today.Date).Days;

            if (daysAway <= 0) return "Coming up soon: happening today";
            if (daysAway == 1) return "Coming up soon: happening tomorrow";

            return "Coming up soon: happening in " + daysAway + " days";
        }
    }
}
