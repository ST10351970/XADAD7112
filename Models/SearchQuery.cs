using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Models
{
    /// <summary>
    /// One search a resident ran on the Local Events page: an optional keyword,
    /// category and date range. Instances are immutable, so they can be kept safely
    /// in the Back stack and the search history queue without being changed later.
    /// </summary>
    public sealed class SearchQuery
    {
        public string Keyword { get; }

        // null means "all categories"
        public string? Category { get; }

        // null means "no limit" on that side of the range
        public DateTime? FromDate { get; }
        public DateTime? ToDate { get; }

        public DateTime SearchedAt { get; }

        /// <summary>
        /// A query with no filters, which lists every upcoming event.
        /// </summary>
        public static SearchQuery Empty { get; } = new SearchQuery(null, null, null, null);

        public SearchQuery(string? keyword, string? category, DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate.HasValue && toDate.HasValue && fromDate.Value.Date > toDate.Value.Date)
            {
                throw new ArgumentException("The start date must be on or before the end date.");
            }

            Keyword = keyword == null ? string.Empty : keyword.Trim();
            Category = string.IsNullOrWhiteSpace(category) ? null : category.Trim();
            FromDate = fromDate.HasValue ? fromDate.Value.Date : (DateTime?)null;
            ToDate = toDate.HasValue ? toDate.Value.Date : (DateTime?)null;
            SearchedAt = DateTime.Now;
        }

        public bool HasKeyword
        {
            get { return Keyword.Length > 0; }
        }

        public bool HasCategory
        {
            get { return Category != null; }
        }

        public bool HasDateRange
        {
            get { return FromDate.HasValue || ToDate.HasValue; }
        }

        public bool IsEmpty
        {
            get { return !HasKeyword && !HasCategory && !HasDateRange; }
        }

        /// <summary>
        /// True when two queries ask for the same thing, ignoring when they were run.
        /// Used to avoid pushing the same search onto the Back stack twice in a row.
        /// </summary>
        public bool IsSameSearchAs(SearchQuery? other)
        {
            if (other == null) return false;

            return string.Equals(Keyword, other.Keyword, StringComparison.OrdinalIgnoreCase)
                && string.Equals(Category, other.Category, StringComparison.OrdinalIgnoreCase)
                && FromDate == other.FromDate
                && ToDate == other.ToDate;
        }

        /// <summary>
        /// A short, readable summary such as: "clinic" in Health & Wellness, 20 Oct - 31 Oct
        /// </summary>
        public string Describe()
        {
            if (IsEmpty)
            {
                return "All upcoming events";
            }

            List<string> parts = new List<string>();

            if (HasKeyword && HasCategory)
            {
                parts.Add("\"" + Keyword + "\" in " + Category);
            }
            else if (HasKeyword)
            {
                parts.Add("\"" + Keyword + "\"");
            }
            else if (Category != null)
            {
                parts.Add(Category);
            }

            if (FromDate.HasValue && ToDate.HasValue)
            {
                parts.Add(FromDate.Value.ToString("dd MMM") + " - " + ToDate.Value.ToString("dd MMM"));
            }
            else if (FromDate.HasValue)
            {
                parts.Add("from " + FromDate.Value.ToString("dd MMM"));
            }
            else if (ToDate.HasValue)
            {
                parts.Add("until " + ToDate.Value.ToString("dd MMM"));
            }

            return string.Join(", ", parts);
        }

        public override string ToString()
        {
            return Describe();
        }
    }
}
