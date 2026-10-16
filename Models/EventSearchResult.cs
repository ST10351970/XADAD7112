using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Models
{
    /// <summary>
    /// The outcome of a search: matching events grouped by day in date order,
    /// plus a set of the matching event IDs for quick "is this in the results?"
    /// checks (used later to avoid recommending events already on screen).
    /// </summary>
    public class EventSearchResult
    {
        private readonly SortedDictionary<DateTime, List<LocalEvent>> _eventsByDate;
        private readonly HashSet<int> _eventIds;

        public SearchQuery Query { get; }

        public EventSearchResult(SearchQuery query, SortedDictionary<DateTime, List<LocalEvent>> eventsByDate)
        {
            Query = query ?? throw new ArgumentNullException(nameof(query));
            _eventsByDate = eventsByDate ?? throw new ArgumentNullException(nameof(eventsByDate));

            _eventIds = new HashSet<int>();
            foreach (List<LocalEvent> day in _eventsByDate.Values)
            {
                foreach (LocalEvent localEvent in day)
                {
                    _eventIds.Add(localEvent.Id);
                }
            }
        }

        /// <summary>
        /// Matching events grouped by calendar day. Enumerating it returns the
        /// days in chronological order, ready to display under date headings.
        /// </summary>
        public IReadOnlyDictionary<DateTime, List<LocalEvent>> EventsByDate
        {
            get { return _eventsByDate; }
        }

        public int TotalCount
        {
            get { return _eventIds.Count; }
        }

        public int DayCount
        {
            get { return _eventsByDate.Count; }
        }

        public bool IsEmpty
        {
            get { return _eventIds.Count == 0; }
        }

        public IReadOnlySet<int> EventIds
        {
            get { return _eventIds; }
        }

        public bool Contains(int eventId)
        {
            return _eventIds.Contains(eventId);
        }

        /// <summary>
        /// Every matching event in date and time order.
        /// </summary>
        public List<LocalEvent> ToList()
        {
            List<LocalEvent> all = new List<LocalEvent>(TotalCount);
            foreach (List<LocalEvent> day in _eventsByDate.Values)
            {
                all.AddRange(day);
            }
            return all;
        }
    }
}
