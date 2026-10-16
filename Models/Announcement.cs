using System;

namespace MunicipalServicesApp.Models
{
    /// <summary>
    /// A service notice from the municipality, such as a water outage, planned
    /// electricity maintenance or a road closure.
    /// </summary>
    public class Announcement
    {
        public int Id { get; }
        public string Title { get; }
        public string Message { get; }
        public string Category { get; }
        public AnnouncementPriority Priority { get; }
        public string Area { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public Announcement(int id, string title, string message, string category,
                            AnnouncementPriority priority, string area,
                            DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("An announcement must have a title.", nameof(title));
            }

            if (endDate.Date < startDate.Date)
            {
                throw new ArgumentException("An announcement cannot end before it starts.", nameof(endDate));
            }

            Id = id;
            Title = title.Trim();
            Message = message ?? string.Empty;
            Category = string.IsNullOrWhiteSpace(category) ? EventCategories.GeneralNotices : category.Trim();
            Priority = priority;
            Area = area ?? string.Empty;
            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// True while the notice is still relevant: it is either running now or
        /// scheduled for a later date. Notices whose end date has passed are hidden.
        /// </summary>
        public bool IsActiveOn(DateTime date)
        {
            return date.Date <= EndDate.Date;
        }

        /// <summary>
        /// True when the notice applies on the given day (as opposed to being
        /// scheduled for later).
        /// </summary>
        public bool IsInEffectOn(DateTime date)
        {
            return StartDate.Date <= date.Date && date.Date <= EndDate.Date;
        }

        public string PriorityLabel
        {
            get
            {
                switch (Priority)
                {
                    case AnnouncementPriority.Critical: return "Urgent";
                    case AnnouncementPriority.High: return "Important";
                    default: return "Notice";
                }
            }
        }

        public string DateRangeText
        {
            get
            {
                if (StartDate.Date == EndDate.Date)
                {
                    return StartDate.ToString("ddd dd MMM");
                }

                return string.Format("{0:dd MMM} - {1:dd MMM}", StartDate, EndDate);
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1} ({2})", PriorityLabel, Title, DateRangeText);
        }
    }
}
