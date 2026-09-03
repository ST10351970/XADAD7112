using System;
using System.Collections.Generic;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Services
{
    // Stores and manages all reported issues for the application session.
    public static class IssueManager
    {
        private static readonly List<Issue> Issues = new List<Issue>();

        private static int _sequence = 0;

        // Number of issues reported
        public static int Count
        {
            get { return Issues.Count; }
        }

        public static string AddIssue(Issue issue)
        {
            if (issue == null)
            {
                throw new ArgumentNullException("issue");
            }

            _sequence++;
            issue.ReferenceNumber = string.Format("MSA-{0}-{1:D4}", DateTime.Now.Year, _sequence);
            Issues.Add(issue);

            return issue.ReferenceNumber;
        }

        public static IReadOnlyList<Issue> GetAllIssues()
        {
            return Issues.AsReadOnly();
        }

        //Returns issues matching a category
        public static List<Issue> GetIssuesByCategory(string category)
        {
            return Issues.FindAll(i =>
                string.Equals(i.Category, category, StringComparison.OrdinalIgnoreCase));
        }
    }
}