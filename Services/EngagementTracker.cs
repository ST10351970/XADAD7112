namespace MunicipalServicesApp.Services
{
    /// <summary>
    /// Implements the chosen user engagement strategy: gamification through
    /// progressive feedback. Tracks how complete a report is and produces an
    /// encouraging message that names the next useful step.
    /// </summary>
    public static class EngagementTracker
    {
        //number of stages for complete report
        public const int TotalSteps = 4;

        //stages of the report the resident has completed.
        public static int CountCompletedSteps(bool hasLocation, bool hasCategory,
                                              bool hasDescription, bool hasAttachment)
        {
            int completed = 0;

            if (hasLocation) completed++;
            if (hasCategory) completed++;
            if (hasDescription) completed++;
            if (hasAttachment) completed++;

            return completed;
        }

        public static int CalculatePercentage(int completedSteps)
        {
            if (completedSteps <= 0) return 0;
            if (completedSteps >= TotalSteps) return 100;

            return (completedSteps * 100) / TotalSteps;
        }

        public static string GetProgressMessage(bool hasLocation, bool hasCategory,
                                                bool hasDescription, bool hasAttachment)
        {
            int completed = CountCompletedSteps(hasLocation, hasCategory,
                                                hasDescription, hasAttachment);

            if (completed == 0)
            {
                return "Let's get started. Begin by entering where the issue is.";
            }

            if (!hasLocation)
            {
                return "Good start. Add the location so the team knows where to go.";
            }

            if (!hasCategory)
            {
                return "Nice work. Now choose the category that best fits the issue.";
            }

            if (!hasDescription)
            {
                return "Almost there. Describe the issue so the team knows what to expect.";
            }

            if (!hasAttachment)
            {
                return "Your report is ready to submit. A photo would make it even stronger.";
            }

            return "Excellent. Your report is complete and ready to submit.";
        }

        public static string GetSubmissionAcknowledgement(int totalReportsThisSession)
        {
            if (totalReportsThisSession <= 1)
            {
                return "That is your first report. Thank you for helping improve your community.";
            }

            return "That is " + totalReportsThisSession +
                   " reports from you this session. Your contribution makes a difference.";
        }
    }
}