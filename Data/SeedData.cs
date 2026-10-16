using System;
using System.Collections.Generic;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Data
{
    /// <summary>
    /// Sample events and announcements loaded when the application starts.
    /// Every date is worked out relative to "today", so the events are always
    /// upcoming no matter when the application is run.
    /// </summary>
    public static class SeedData
    {
        public static List<LocalEvent> CreateEvents(DateTime today)
        {
            DateTime day = today.Date;

            // Builds a start time "days" from today at the given hour and minute.
            DateTime At(int days, int hour, int minute = 0)
            {
                return day.AddDays(days).AddHours(hour).AddMinutes(minute);
            }

            TimeSpan Hours(double hours)
            {
                return TimeSpan.FromHours(hours);
            }

            return new List<LocalEvent>
            {
                new LocalEvent(1, "Ward 12 Budget Feedback Meeting", EventCategories.CommunityMeetings,
                    At(2, 18), Hours(2), "Mamelodi Community Hall", "Mamelodi East",
                    "Your ward councillor reports back on the draft budget and the projects planned for the ward. Bring your questions.",
                    "budget", "IDP", "councillor", "ward", "public participation"),

                new LocalEvent(2, "Water Conservation Workshop", EventCategories.WaterAndSanitation,
                    At(3, 10), Hours(2), "Hatfield Community Library", "Hatfield",
                    "Practical ways to cut household water use, find hidden leaks and read your water meter.",
                    "water", "saving", "leaks", "meter", "workshop"),

                new LocalEvent(3, "Free Eye and Blood Pressure Screening", EventCategories.HealthAndWellness,
                    At(3, 8), Hours(6), "Block H Clinic", "Soshanguve",
                    "Free walk-in screening for eyesight, blood pressure and blood sugar. No appointment needed.",
                    "clinic", "screening", "free", "eyes", "blood pressure"),

                new LocalEvent(4, "Youth Soccer Tournament", EventCategories.SportsAndRecreation,
                    At(5, 9), Hours(8), "Atteridgeville Sports Grounds", "Atteridgeville",
                    "Under-15 and under-19 teams from across the region compete for the municipal youth cup.",
                    "soccer", "football", "youth", "tournament"),

                new LocalEvent(5, "Moreleta Spruit Clean-up", EventCategories.EnvironmentAndWaste,
                    At(5, 8), Hours(4), "Moreleta Spruit Trail (Garsfontein entrance)", "Garsfontein",
                    "Join volunteers to clear litter along the spruit. Gloves and refuse bags are provided.",
                    "clean-up", "volunteers", "litter", "river", "outdoors"),

                new LocalEvent(6, "Electricity Safety Awareness Talk", EventCategories.Electricity,
                    At(6, 17, 30), Hours(1.5), "Akasia Community Centre", "Akasia",
                    "The dangers of illegal connections and meter tampering, and how to report them safely.",
                    "electricity", "safety", "illegal connections", "meter"),

                new LocalEvent(7, "Heritage Craft Market", EventCategories.ArtsAndCulture,
                    At(8, 9), Hours(7), "Sammy Marks Square", "Pretoria CBD",
                    "Local crafters, food stalls and live performances celebrating the region's cultures.",
                    "market", "crafts", "local business", "heritage", "music", "family"),

                new LocalEvent(8, "Neighbourhood Watch Recruitment Evening", EventCategories.PublicSafety,
                    At(9, 18, 30), Hours(2), "Sunnyside Recreation Centre", "Sunnyside",
                    "Meet the local policing forum and find out how to join or start a neighbourhood watch.",
                    "neighbourhood watch", "crime", "volunteers", "safety", "policing forum"),

                new LocalEvent(9, "Bus Route Changes Public Consultation", EventCategories.RoadsAndTransport,
                    At(10, 17), Hours(2), "Centurion Civic Centre", "Centurion",
                    "Comment on proposed changes to municipal bus routes and timetables before they are finalised.",
                    "bus", "routes", "commuters", "public transport", "public participation"),

                new LocalEvent(10, "Recycling and E-waste Drop-off Day", EventCategories.EnvironmentAndWaste,
                    At(12, 8), Hours(5), "Garsfontein Drop-off Centre", "Garsfontein",
                    "Bring paper, glass, plastics, old electronics and batteries for free, responsible disposal.",
                    "recycling", "e-waste", "drop-off", "batteries"),

                new LocalEvent(11, "Prepaid Meter Information Session", EventCategories.Electricity,
                    At(12, 10), Hours(2), "Lynnwood Library Hall", "Lynnwood",
                    "How the new prepaid meters work, how to load tokens and what to do when a meter fails.",
                    "prepaid", "meter", "electricity", "billing", "tokens"),

                new LocalEvent(12, "Ward 45 Councillor Open Day", EventCategories.CommunityMeetings,
                    At(13, 10), Hours(3), "Montana Community Hall", "Montana",
                    "Drop in to raise service delivery concerns directly with your councillor and ward committee.",
                    "councillor", "ward", "service delivery", "complaints"),

                new LocalEvent(13, "Child Immunisation Catch-up Drive", EventCategories.HealthAndWellness,
                    At(14, 8), Hours(8), "Mamelodi West Clinic", "Mamelodi West",
                    "Missed vaccinations for children under six. Bring the child's Road to Health booklet.",
                    "children", "vaccines", "immunisation", "clinic", "family"),

                new LocalEvent(14, "Senior Citizens Fun Walk", EventCategories.SportsAndRecreation,
                    At(15, 7), Hours(3), "Centurion Lake", "Centurion",
                    "A gentle 3 km walk around the lake followed by free health checks and refreshments.",
                    "seniors", "walk", "fitness", "outdoors", "health"),

                new LocalEvent(15, "Community Theatre Festival", EventCategories.ArtsAndCulture,
                    At(17, 18), Hours(3), "Arcadia Community Theatre", "Arcadia",
                    "Three short plays by local youth drama groups. Free entry, seating is first come, first served.",
                    "theatre", "drama", "youth", "performance"),

                new LocalEvent(16, "Fire Safety and Shack Fire Prevention Demo", EventCategories.PublicSafety,
                    At(18, 10), Hours(2), "Soshanguve Fire Station", "Soshanguve",
                    "Firefighters demonstrate safe paraffin and gas use and how to stop a fire from spreading.",
                    "fire", "safety", "prevention", "informal settlements"),

                new LocalEvent(17, "Road Maintenance Programme Briefing", EventCategories.RoadsAndTransport,
                    At(19, 18), Hours(1.5), "Garsfontein Community Hall", "Garsfontein",
                    "Which roads are scheduled for pothole repair and resurfacing this quarter, and expected delays.",
                    "potholes", "road works", "maintenance", "traffic"),

                new LocalEvent(18, "Water Quality Information Day", EventCategories.WaterAndSanitation,
                    At(20, 9), Hours(3), "Atteridgeville Community Hall", "Atteridgeville",
                    "Officials explain how tap water is tested. Bring a sample from home for a free basic test.",
                    "water quality", "testing", "tap water", "water"),

                new LocalEvent(19, "Community Tree Planting Day", EventCategories.EnvironmentAndWaste,
                    At(22, 8, 30), Hours(3), "Mamelodi Nature Reserve", "Mamelodi",
                    "Help plant 500 indigenous trees with local schools. Spades and saplings are provided.",
                    "trees", "greening", "volunteers", "schools", "outdoors"),

                new LocalEvent(20, "Draft Tariffs Public Participation Meeting", EventCategories.CommunityMeetings,
                    At(24, 18), Hours(2), "Lynnwood Library Hall", "Lynnwood",
                    "Have your say on proposed water, electricity and refuse tariffs for the next financial year.",
                    "tariffs", "budget", "rates", "public participation"),

                new LocalEvent(21, "Mental Wellness Walk and Talk", EventCategories.HealthAndWellness,
                    At(26, 7, 30), Hours(2), "Magnolia Dell Park", "Pretoria Central",
                    "A relaxed group walk with counsellors on hand to talk about stress and where to find support.",
                    "wellness", "mental health", "walk", "support", "outdoors"),

                new LocalEvent(22, "Netball League Final", EventCategories.SportsAndRecreation,
                    At(27, 13), Hours(4), "Mamelodi Sports Complex", "Mamelodi",
                    "The top two teams in the community netball league play for the title. Food stalls on site.",
                    "netball", "league", "final", "women"),

                new LocalEvent(23, "Jazz in the Park", EventCategories.ArtsAndCulture,
                    At(29, 14), Hours(4), "Magnolia Dell Park", "Pretoria Central",
                    "An afternoon of local jazz bands. Bring a blanket and a picnic. Free entry.",
                    "jazz", "music", "picnic", "family", "outdoors"),

                new LocalEvent(24, "Sewer Network Upgrade Briefing", EventCategories.WaterAndSanitation,
                    At(31, 17, 30), Hours(1.5), "Sunnyside Recreation Centre", "Sunnyside",
                    "Engineers explain the upcoming sewer pipe replacement, road closures and the expected timeline.",
                    "sewer", "sanitation", "upgrade", "construction", "road works"),

                new LocalEvent(25, "Learner Driver Road Safety Workshop", EventCategories.RoadsAndTransport,
                    At(33, 9), Hours(3), "Akasia Licensing Centre", "Akasia",
                    "Preparing for the learner's licence test and staying safe as a new driver.",
                    "road safety", "learner's licence", "youth", "driving"),

                new LocalEvent(26, "Electricity Outage Schedule Feedback Meeting", EventCategories.Electricity,
                    At(35, 18), Hours(2), "Soshanguve Community Hall", "Soshanguve",
                    "Residents give feedback on outage schedules and hear about planned network upgrades.",
                    "outages", "schedule", "electricity", "upgrades", "public participation"),

                new LocalEvent(27, "Home and Vehicle Safety Briefing", EventCategories.PublicSafety,
                    At(38, 18), Hours(1.5), "Centurion Civic Centre", "Centurion",
                    "Metro police and SAPS share practical tips on keeping homes, cars and families safe, especially when travelling.",
                    "home security", "safety", "crime prevention", "travel")
            };
        }

        public static List<Announcement> CreateAnnouncements(DateTime today)
        {
            DateTime day = today.Date;

            return new List<Announcement>
            {
                new Announcement(1, "Unplanned water outage: burst main",
                    "A burst bulk water main is being repaired. Water tankers are stationed at Mamelodi Community Hall and the Mamelodi East taxi rank.",
                    EventCategories.WaterAndSanitation, AnnouncementPriority.Critical, "Mamelodi East",
                    day, day.AddDays(1)),

                new Announcement(2, "Boil water advisory",
                    "Following a water quality alert, boil all tap water for at least one minute before drinking or cooking until further notice.",
                    EventCategories.WaterAndSanitation, AnnouncementPriority.Critical, "Atteridgeville",
                    day, day.AddDays(3)),

                new Announcement(3, "Planned electricity maintenance",
                    "Electricity will be switched off from 08:00 to 16:00 for substation maintenance. Treat all lines as live.",
                    EventCategories.Electricity, AnnouncementPriority.High, "Hatfield and Arcadia",
                    day.AddDays(4), day.AddDays(4)),

                new Announcement(4, "Road closure for resurfacing",
                    "Lynnwood Road will be closed to traffic between Duxbury Road and Roper Street. Use Duncan Street as a detour.",
                    EventCategories.RoadsAndTransport, AnnouncementPriority.High, "Lynnwood",
                    day.AddDays(2), day.AddDays(9)),

                new Announcement(5, "Refuse collection day change",
                    "Refuse in Centurion will be collected on Thursdays instead of Wednesdays for the next two weeks.",
                    EventCategories.EnvironmentAndWaste, AnnouncementPriority.Normal, "Centurion",
                    day.AddDays(1), day.AddDays(14)),

                new Announcement(6, "Clinic extended hours",
                    "Block H Clinic is open until 19:00 on weekdays this month to reduce waiting times.",
                    EventCategories.HealthAndWellness, AnnouncementPriority.Normal, "Soshanguve",
                    day, day.AddDays(30)),

                new Announcement(7, "Library closed for renovations",
                    "Hatfield Community Library is closed for renovations. The Water Conservation Workshop will still take place in the library hall.",
                    EventCategories.ArtsAndCulture, AnnouncementPriority.Normal, "Hatfield",
                    day, day.AddDays(21)),

                new Announcement(8, "Pay your municipal account online",
                    "Municipal accounts can now be paid online and at selected retailers. Customer care offices remain open on weekdays.",
                    EventCategories.GeneralNotices, AnnouncementPriority.Normal, "All areas",
                    day, day.AddDays(45)),

                // Already ended: kept to show that expired notices are filtered out.
                new Announcement(9, "Water supply restored",
                    "Water supply to Sunnyside has been fully restored following last week's pipe repairs.",
                    EventCategories.WaterAndSanitation, AnnouncementPriority.Normal, "Sunnyside",
                    day.AddDays(-5), day.AddDays(-1))
            };
        }
    }
}
