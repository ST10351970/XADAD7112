using System;

namespace MunicipalServicesApp.Models
{
    /// <summary>
    /// Represents a single issue reported by a resident.
    /// </summary>
    public class Issue
    {
        public string ReferenceNumber { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime DateReported { get; set; }

        public Issue()
        {
            DateReported = DateTime.Now;
        }

        public Issue(string location, string category, string description, string attachmentPath)
        {
            Location = location;
            Category = category;
            Description = description;
            AttachmentPath = attachmentPath;
            DateReported = DateTime.Now;
        }

        /// <summary>
        /// True when the resident attached a supporting file to the report.
        /// </summary>
        public bool HasAttachment
        {
            get { return !string.IsNullOrEmpty(AttachmentPath); }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} ({2})", ReferenceNumber, Category, Location);
        }
    }
}