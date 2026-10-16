namespace MunicipalServicesApp.Models
{
    /// <summary>
    /// How urgent an announcement is. Lower numbers are more urgent, which matches
    /// the min-heap ordering of .NET's PriorityQueue: Critical is dequeued first.
    /// </summary>
    public enum AnnouncementPriority
    {
        Critical = 0,
        High = 1,
        Normal = 2
    }
}
