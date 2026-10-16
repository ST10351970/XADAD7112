using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Models
{
    /// <summary>
    /// A municipal event residents can attend, such as a ward meeting, a clinic
    /// screening day or a community clean-up.
    /// </summary>
    public class LocalEvent
    {
        public int Id { get; }
        public string Title { get; }
        public string Category { get; }
        public DateTime StartDateTime { get; }
        public DateTime EndDateTime { get; }
        public string Venue { get; }
        public string Area { get; }
        public string Description { get; }

        /// <summary>
        /// Keywords describing the event. Stored in a case-insensitive HashSet so
        /// each tag is unique and "Water" and "water" count as the same tag. The
        /// recommendation feature compares tag sets between events.
        /// </summary>
        public IReadOnlySet<string> Tags { get; }

        public LocalEvent(int id, string title, string category, DateTime startDateTime,
                          TimeSpan duration, string venue, string area, string description,
                          params string[] tags)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("An event must have a title.", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException("An event must have a category.", nameof(category));
            }

            if (duration <= TimeSpan.Zero)
            {
                throw new ArgumentException("An event must have a positive duration.", nameof(duration));
            }

            Id = id;
            Title = title.Trim();
            Category = category.Trim();
            StartDateTime = startDateTime;
            EndDateTime = startDateTime.Add(duration);
            Venue = venue ?? string.Empty;
            Area = area ?? string.Empty;
            Description = description ?? string.Empty;

            HashSet<string> tagSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (tags != null)
            {
                foreach (string tag in tags)
                {
                    if (!string.IsNullOrWhiteSpace(tag))
                    {
                        tagSet.Add(tag.Trim());
                    }
                }
            }
            Tags = tagSet;
        }

        /// <summary>
        /// The calendar day of the event with no time part. This is the key used
        /// to group events in EventManager's sorted dictionary.
        /// </summary>
        public DateTime Date
        {
            get { return StartDateTime.Date; }
        }

        public string TimeRangeText
        {
            get { return string.Format("{0:HH:mm} - {1:HH:mm}", StartDateTime, EndDateTime); }
        }

        public string LocationText
        {
            get
            {
                if (string.IsNullOrEmpty(Area)) return Venue;
                if (string.IsNullOrEmpty(Venue)) return Area;
                return Venue + ", " + Area;
            }
        }

        /// <summary>
        /// True when every word in the keyword appears somewhere in the event's
        /// title, description, venue, area, category or tags. An empty keyword
        /// matches everything.
        /// </summary>
        public bool MatchesKeyword(string? keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return true;
            }

            string[] terms = keyword.Split(new[] { ' ', ',' },
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (string term in terms)
            {
                if (!ContainsTerm(term))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ContainsTerm(string term)
        {
            if (Title.Contains(term, StringComparison.OrdinalIgnoreCase)) return true;
            if (Description.Contains(term, StringComparison.OrdinalIgnoreCase)) return true;
            if (Venue.Contains(term, StringComparison.OrdinalIgnoreCase)) return true;
            if (Area.Contains(term, StringComparison.OrdinalIgnoreCase)) return true;
            if (Category.Contains(term, StringComparison.OrdinalIgnoreCase)) return true;

            foreach (string tag in Tags)
            {
                if (tag.Contains(term, StringComparison.OrdinalIgnoreCase)) return true;
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("{0:ddd dd MMM HH:mm} - {1} ({2})", StartDateTime, Title, Category);
        }
    }
}
