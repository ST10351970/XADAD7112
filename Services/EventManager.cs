using System;
using System.Collections.Generic;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Services
{
    /// <summary>
    /// Stores local events and announcements and keeps an index for each kind of
    /// lookup the Local Events page needs, so no lookup has to scan everything.
    ///
    ///   SortedDictionary&lt;DateTime, List&lt;LocalEvent&gt;&gt;  events grouped by day, always in date order
    ///   Dictionary&lt;string, List&lt;LocalEvent&gt;&gt;      category  -> events, O(1) category filter
    ///   Dictionary&lt;int, LocalEvent&gt;                 event ID  -> event, O(1) lookup by ID
    ///   HashSet&lt;string&gt;                              unique categories for the filter list
    ///   HashSet&lt;DateTime&gt;                            unique days that have events, for the calendar
    ///   PriorityQueue&lt;Announcement, ...&gt;             announcements, most urgent first
    ///
    /// Every event is added through AddEvent, which updates all of the indexes
    /// together so they can never disagree with each other.
    /// </summary>
    public class EventManager
    {
        private static readonly Lazy<EventManager> SharedInstance = new Lazy<EventManager>(CreateSeeded);

        /// <summary>
        /// The single instance used by the application, loaded with the seed data
        /// the first time it is needed. It lives for the whole session, like
        /// IssueManager, so search history survives closing and reopening the page.
        /// </summary>
        public static EventManager Shared
        {
            get { return SharedInstance.Value; }
        }

        // Primary index. Keys are calendar days (no time part); each day's list is
        // kept in start-time order. A SortedDictionary is a balanced binary search
        // tree, so enumerating it always returns the days in chronological order
        // and inserting a new day costs O(log n).
        private readonly SortedDictionary<DateTime, List<LocalEvent>> _eventsByDate =
            new SortedDictionary<DateTime, List<LocalEvent>>();

        // Hash table indexes: average O(1) lookup by category or by ID.
        private readonly Dictionary<string, List<LocalEvent>> _eventsByCategory =
            new Dictionary<string, List<LocalEvent>>(StringComparer.OrdinalIgnoreCase);

        private readonly Dictionary<int, LocalEvent> _eventsById =
            new Dictionary<int, LocalEvent>();

        // Sets: each category and each date is stored once, however many events use it.
        private readonly HashSet<string> _categories =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        private readonly HashSet<DateTime> _eventDates = new HashSet<DateTime>();

        // Min-heap ordered by (priority, start date): Critical (0) before High (1)
        // before Normal (2), and the sooner notice first when priorities tie.
        private readonly PriorityQueue<Announcement, (int Priority, DateTime StartDate)> _announcements =
            new PriorityQueue<Announcement, (int Priority, DateTime StartDate)>();

        public EventManager()
        {
        }

        public EventManager(IEnumerable<LocalEvent> events, IEnumerable<Announcement> announcements)
        {
            if (events != null)
            {
                foreach (LocalEvent localEvent in events)
                {
                    AddEvent(localEvent);
                }
            }

            if (announcements != null)
            {
                foreach (Announcement announcement in announcements)
                {
                    AddAnnouncement(announcement);
                }
            }
        }

        private static EventManager CreateSeeded()
        {
            DateTime today = DateTime.Today;
            return new EventManager(SeedData.CreateEvents(today), SeedData.CreateAnnouncements(today));
        }

        public int EventCount
        {
            get { return _eventsById.Count; }
        }

        public int AnnouncementCount
        {
            get { return _announcements.Count; }
        }

        public int CategoryCount
        {
            get { return _categories.Count; }
        }

        // ------------------------------------------------------------------
        // Adding data
        // ------------------------------------------------------------------

        /// <summary>
        /// Adds an event and updates every index.
        /// </summary>
        public void AddEvent(LocalEvent localEvent)
        {
            if (localEvent == null)
            {
                throw new ArgumentNullException(nameof(localEvent));
            }

            // TryAdd checks for a duplicate ID and adds in a single hash lookup.
            if (!_eventsById.TryAdd(localEvent.Id, localEvent))
            {
                throw new InvalidOperationException(
                    string.Format("An event with ID {0} has already been added.", localEvent.Id));
            }

            if (!_eventsByDate.TryGetValue(localEvent.Date, out List<LocalEvent>? sameDay))
            {
                sameDay = new List<LocalEvent>();
                _eventsByDate.Add(localEvent.Date, sameDay);
            }
            InsertInStartTimeOrder(sameDay, localEvent);

            if (!_eventsByCategory.TryGetValue(localEvent.Category, out List<LocalEvent>? sameCategory))
            {
                sameCategory = new List<LocalEvent>();
                _eventsByCategory.Add(localEvent.Category, sameCategory);
            }
            InsertInStartTimeOrder(sameCategory, localEvent);

            // HashSet.Add ignores values it already holds, so no duplicate check is needed.
            _categories.Add(localEvent.Category);
            _eventDates.Add(localEvent.Date);
        }

        /// <summary>
        /// Adds an announcement to the priority queue. Its category also joins the
        /// category set, so the filter list covers events and announcements alike.
        /// </summary>
        public void AddAnnouncement(Announcement announcement)
        {
            if (announcement == null)
            {
                throw new ArgumentNullException(nameof(announcement));
            }

            _announcements.Enqueue(announcement, ((int)announcement.Priority, announcement.StartDate));
            _categories.Add(announcement.Category);
        }

        // Keeps a list sorted by start time as items are added. The list is
        // already sorted, so a binary search finds the insert position.
        private static void InsertInStartTimeOrder(List<LocalEvent> list, LocalEvent localEvent)
        {
            int low = 0;
            int high = list.Count;

            while (low < high)
            {
                int middle = (low + high) / 2;
                if (list[middle].StartDateTime <= localEvent.StartDateTime)
                {
                    low = middle + 1;
                }
                else
                {
                    high = middle;
                }
            }

            list.Insert(low, localEvent);
        }

        // ------------------------------------------------------------------
        // Lookups
        // ------------------------------------------------------------------

        public LocalEvent? GetEventById(int id)
        {
            return _eventsById.TryGetValue(id, out LocalEvent? localEvent) ? localEvent : null;
        }

        /// <summary>
        /// The unique categories in alphabetical order, for the category filter.
        /// </summary>
        public List<string> GetCategories()
        {
            List<string> categories = new List<string>(_categories);
            categories.Sort(StringComparer.OrdinalIgnoreCase);
            return categories;
        }

        /// <summary>
        /// Only the categories that have at least one event, in alphabetical order.
        /// Uses the keys of the category dictionary, which never include
        /// announcement-only categories such as General Notices.
        /// </summary>
        public List<string> GetEventCategories()
        {
            List<string> categories = new List<string>(_eventsByCategory.Keys);
            categories.Sort(StringComparer.OrdinalIgnoreCase);
            return categories;
        }

        public int GetEventCountForCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category)) return 0;

            return _eventsByCategory.TryGetValue(category, out List<LocalEvent>? events) ? events.Count : 0;
        }

        /// <summary>
        /// Every day that has at least one event. Intended for MonthCalendar.BoldedDates.
        /// </summary>
        public DateTime[] GetEventDates()
        {
            DateTime[] dates = new DateTime[_eventDates.Count];
            _eventDates.CopyTo(dates);
            Array.Sort(dates);
            return dates;
        }

        /// <summary>
        /// O(1) check against the date set: does anything happen on this day?
        /// </summary>
        public bool HasEventsOn(DateTime date)
        {
            return _eventDates.Contains(date.Date);
        }

        public List<LocalEvent> GetEventsOn(DateTime date)
        {
            return _eventsByDate.TryGetValue(date.Date, out List<LocalEvent>? events)
                ? new List<LocalEvent>(events)
                : new List<LocalEvent>();
        }

        public List<LocalEvent> GetEventsByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category)) return new List<LocalEvent>();

            return _eventsByCategory.TryGetValue(category, out List<LocalEvent>? events)
                ? new List<LocalEvent>(events)
                : new List<LocalEvent>();
        }

        /// <summary>
        /// Events from fromDate to toDate inclusive, in date and time order.
        /// </summary>
        public List<LocalEvent> GetEventsInRange(DateTime fromDate, DateTime toDate)
        {
            List<LocalEvent> results = new List<LocalEvent>();

            foreach (List<LocalEvent> day in EnumerateDays(fromDate.Date, toDate.Date))
            {
                results.AddRange(day);
            }

            return results;
        }

        /// <summary>
        /// The next events from the given date onwards, soonest first.
        /// </summary>
        public List<LocalEvent> GetUpcomingEvents(DateTime fromDate, int maxCount)
        {
            List<LocalEvent> results = new List<LocalEvent>();
            if (maxCount <= 0) return results;

            foreach (List<LocalEvent> day in EnumerateDays(fromDate.Date, DateTime.MaxValue.Date))
            {
                foreach (LocalEvent localEvent in day)
                {
                    results.Add(localEvent);
                    if (results.Count == maxCount) return results;
                }
            }

            return results;
        }

        // Walks the sorted dictionary from fromDate to toDate. The keys come out in
        // ascending order, so the loop stops at the first day after toDate instead
        // of checking every remaining day.
        private IEnumerable<List<LocalEvent>> EnumerateDays(DateTime fromDate, DateTime toDate)
        {
            foreach (KeyValuePair<DateTime, List<LocalEvent>> day in _eventsByDate)
            {
                if (day.Key < fromDate) continue;
                if (day.Key > toDate) yield break;

                yield return day.Value;
            }
        }

        // ------------------------------------------------------------------
        // Search
        // ------------------------------------------------------------------

        public EventSearchResult Search(SearchQuery query)
        {
            return Search(query, DateTime.Today);
        }

        /// <summary>
        /// Finds upcoming events that match every filter in the query. When a
        /// category is chosen the search starts from the category index, which is
        /// usually much smaller than the full list; otherwise it walks the date
        /// index between the requested dates. Results are grouped by day in a new
        /// SortedDictionary so they come back in chronological order.
        /// </summary>
        public EventSearchResult Search(SearchQuery query, DateTime today)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            DateTime fromDate = query.FromDate ?? today.Date;
            DateTime toDate = query.ToDate ?? DateTime.MaxValue.Date;

            SortedDictionary<DateTime, List<LocalEvent>> matches =
                new SortedDictionary<DateTime, List<LocalEvent>>();

            if (query.Category != null)
            {
                if (_eventsByCategory.TryGetValue(query.Category, out List<LocalEvent>? inCategory))
                {
                    // The category list is in date order, so stop once past toDate.
                    foreach (LocalEvent localEvent in inCategory)
                    {
                        if (localEvent.Date > toDate) break;
                        if (localEvent.Date < fromDate) continue;

                        if (localEvent.MatchesKeyword(query.Keyword))
                        {
                            AddToDay(matches, localEvent);
                        }
                    }
                }
            }
            else
            {
                foreach (List<LocalEvent> day in EnumerateDays(fromDate, toDate))
                {
                    foreach (LocalEvent localEvent in day)
                    {
                        if (localEvent.MatchesKeyword(query.Keyword))
                        {
                            AddToDay(matches, localEvent);
                        }
                    }
                }
            }

            return new EventSearchResult(query, matches);
        }

        private static void AddToDay(SortedDictionary<DateTime, List<LocalEvent>> days, LocalEvent localEvent)
        {
            if (!days.TryGetValue(localEvent.Date, out List<LocalEvent>? list))
            {
                list = new List<LocalEvent>();
                days.Add(localEvent.Date, list);
            }

            // Events reach this method in start-time order, so appending keeps each day sorted.
            list.Add(localEvent);
        }

        // ------------------------------------------------------------------
        // Announcements
        // ------------------------------------------------------------------

        public List<Announcement> GetActiveAnnouncements()
        {
            return GetActiveAnnouncements(DateTime.Today);
        }

        /// <summary>
        /// Announcements that have not yet ended, most urgent first. Enumerating a
        /// PriorityQueue directly returns items in no particular order, so this
        /// copies the queue and dequeues from the copy; the stored queue is unchanged.
        /// </summary>
        public List<Announcement> GetActiveAnnouncements(DateTime today)
        {
            return GetActiveAnnouncements(today, null);
        }

        /// <summary>
        /// Active announcements in one category (or all categories when category is
        /// null), most urgent first.
        /// </summary>
        public List<Announcement> GetActiveAnnouncements(DateTime today, string? category)
        {
            PriorityQueue<Announcement, (int Priority, DateTime StartDate)> working =
                new PriorityQueue<Announcement, (int Priority, DateTime StartDate)>(_announcements.UnorderedItems);

            List<Announcement> ordered = new List<Announcement>(working.Count);

            while (working.TryDequeue(out Announcement? announcement, out _))
            {
                if (!announcement.IsActiveOn(today)) continue;

                if (category != null &&
                    !string.Equals(announcement.Category, category, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                ordered.Add(announcement);
            }

            return ordered;
        }

        /// <summary>
        /// The single most urgent active announcement, or null if there are none.
        /// </summary>
        public Announcement? GetMostUrgentAnnouncement(DateTime today)
        {
            List<Announcement> active = GetActiveAnnouncements(today);
            return active.Count > 0 ? active[0] : null;
        }
    }
}
