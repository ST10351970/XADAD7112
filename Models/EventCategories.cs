namespace MunicipalServicesApp.Models
{
    /// <summary>
    /// Category names shared by events and announcements. The constants keep the
    /// seed data consistent; the list shown to residents is built from the data
    /// itself (see EventManager.GetCategories), so a category only appears in the
    /// search filter if something actually uses it.
    /// </summary>
    public static class EventCategories
    {
        public const string CommunityMeetings = "Community Meetings";
        public const string WaterAndSanitation = "Water & Sanitation";
        public const string Electricity = "Electricity";
        public const string HealthAndWellness = "Health & Wellness";
        public const string SportsAndRecreation = "Sports & Recreation";
        public const string ArtsAndCulture = "Arts & Culture";
        public const string EnvironmentAndWaste = "Environment & Waste";
        public const string PublicSafety = "Public Safety";
        public const string RoadsAndTransport = "Roads & Transport";

        // Used by announcements only, e.g. account and office notices.
        public const string GeneralNotices = "General Notices";
    }
}
