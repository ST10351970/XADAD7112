# Municipal Services Application

A Windows Forms application that allows South African residents to report municipal
service issues, such as water leaks, potholes and uncollected refuse, and to find local
events and service notices from their municipality.

This README covers Part 2 of the Portfolio of Evidence. Two services are available:

- **Report Issues** (Part 1): report a problem and attach a supporting file.
- **Local Events and Announcements** (Part 2): browse and search upcoming events, read
  service notices in order of urgency, and get event suggestions based on your searches.

**Service Request Status** appears on the main menu but is disabled. It will be
implemented in a later part.

---

## 1. Requirements

| Requirement | Version |
|---|---|
| Operating system | Windows 10 or Windows 11 |
| IDE | Visual Studio 2022 (17.8 or later) |
| Framework | .NET 8.0 (`net8.0-windows`) |
| Workload | .NET desktop development |
| Language | C# |

### Framework note

The brief specifies .NET Framework 4.8. This project targets .NET 8.0 because the Visual
Studio installation used for development offered only .NET 7, 8 and 9. Part 2 also uses
`PriorityQueue<TElement, TPriority>`, which is part of .NET 6 and later and is not
available in .NET Framework 4.8.

---

## 2. How to compile

### Using Visual Studio

1. Open `MunicipalServicesApp.sln` in Visual Studio 2022.
2. Wait for the solution to finish loading in Solution Explorer.
3. Select **Build > Build Solution**, or press **F6**.
4. Confirm that the Output window reports `Build succeeded` with 0 errors.

### Using the command line

From the project folder (it contains both the solution and the project file, so name
the one to build):

```
dotnet build MunicipalServicesApp.sln
```

---

## 3. How to run

### Using Visual Studio

Press **F5** to run with debugging, or **Ctrl + F5** to run without debugging.

### Using the command line

From the project folder:

```
dotnet run --project MunicipalServicesApp.csproj
```

### Running the compiled executable

After a successful build, the executable is located in the project folder at:

```
bin\Debug\net8.0-windows\MunicipalServicesApp.exe
```

---

## 4. How to use the software

### 4.1 Main menu

The main menu appears on startup and presents three municipal services:

- **Report Issues**: available.
- **Local Events and Announcements**: available. The description beside the button
  shows how many upcoming events there are and, when there is one, the most urgent
  active notice marked *Urgent*.
- **Service Request Status**: greyed out, to be implemented later.

A summary at the bottom of the window shows how many issues have been reported during
the current session. Click **Exit** to close the application; a confirmation prompt is
shown before it closes.

### 4.2 Reporting an issue

Click **Report Issues** to open the reporting form, then complete the following:

1. **Location**: type where the issue is, for example `Corner of Main Road and 5th Avenue, Kempton Park`. Required, minimum 3 characters.
2. **Category**: choose from the dropdown list: Sanitation, Roads and Potholes, Water and Sewerage, Electricity, Refuse Removal, Street Lighting, Parks and Public Spaces, or Other. Required.
3. **Description**: describe the issue in the rich text box. Required, minimum 10 characters.
4. **Attachment**: click **Attach Photo or Document** to open a file dialog and select a supporting image or document. Optional, maximum file size 10 MB. Accepted formats are PNG, JPG, JPEG, BMP, GIF, PDF, DOCX, DOC and TXT.

Then click **Submit Report**.

Other controls on this form:

- **Clear**: empties all fields after a confirmation prompt.
- **Back to Main Menu**: closes the form and returns to the main menu.

### 4.3 User engagement feature

The application implements **gamification through progressive feedback** as its user
engagement strategy.

A progress bar at the top of the reporting form fills as each of the four stages is
completed, in 25% increments:

| Stage completed | Progress |
|---|---|
| Location entered | 25% |
| Category selected | 50% |
| Description entered | 75% |
| Attachment added | 100% |

Above the bar, a message updates in real time to acknowledge what has been completed and
name the next useful step, for example *"Almost there. Describe the issue so the team
knows what to expect."*

On successful submission, a confirmation message displays a unique reference number in
the format `MSA-YYYY-NNNN` (for example `MSA-2026-0001`), together with an
acknowledgement of how many reports the resident has submitted during the session.

### 4.4 Validation and feedback

The application will not accept an incomplete report. If a required field is missing or
too short, a message explains what is needed and the cursor is placed in the field that
requires attention. Successful actions, warnings and errors are all communicated through
message boxes.

### 4.5 Local Events and Announcements

Click **Local Events and Announcements** on the main menu. The page has a search bar
across the top and three columns:

| Area | What it shows |
|---|---|
| **Service notices** (left) | Active notices from the municipality, most urgent first. Urgent notices have a red stripe and a tinted background; important notices are amber. Notices that have ended are hidden. |
| **Events** (centre) | Upcoming events grouped under a heading for each day, in date and time order. Each card shows the category (colour coded), time, title, venue, area and a short description. |
| **Calendar and suggestions** (right) | A calendar with days that have events shown in **bold**, and a column of suggested events. |

When the page first opens, every upcoming event is listed.

**Searching**

1. **Keyword**: type one or more words, for example `clinic` or `water meter`. Titles,
   descriptions, venues, areas, categories and topics are searched. When several words are
   entered, an event must match all of them.
2. **Category**: choose a category, or leave it on *All categories*. Choosing a category
   also filters the notices on the left to that category.
3. **From** and **To**: tick the box on a date to use it. Leave a box unticked for no
   limit on that side. The From date must be on or before the To date; if it is not, a
   message explains the problem and the search is not run.
4. Click **Search** or press **Enter**.

The heading above the events describes the search and how many events matched. If nothing
matches, a message suggests how to widen the search.

**Other controls**

- **Calendar**: click a day to see everything happening on that day.
- **Previous search**: goes back one search at a time, restoring the filters as well as
  the results. Hover over it to see which search it will go back to. It is disabled when
  there is nothing to go back to.
- **Clear**: removes all filters and lists every upcoming event again.
- **Event cards**: click a card, or select it with Tab and press Enter, to see the full
  details of the event.
- **Back to Main Menu**, or **Escape**: returns to the main menu.

The status line at the bottom shows how many events are displayed out of the total, and
how many service notices are active.

### 4.6 Recommended events

The right-hand column suggests three events based on what you search for and which events
you open.

- Before you have searched or opened anything, it is headed **Coming up soon** and shows
  the next events on the calendar.
- After you search or open an event, it is headed **Recommended for you**. Each card gives
  the reason for the suggestion, for example *Matches your search for "water"* or
  *Similar topics: budget, public participation*.
- A note at the top of the column says what the suggestions are based on (for example
  *Based on your last 3 searches and 1 event you opened*) and lists your top interests.
  Hover over the **Recommended for you** heading to see your recent searches.
- Events already showing in your search results, and events you have already opened, are
  not suggested.
- Suggestions update as soon as you search or open an event, and are kept when you leave
  the page and come back during the same session.

**To see the feature working:**

1. Open the page and note the **Coming up soon** suggestions.
2. Search for `water`. The suggestions change to events related to the results, such as
   meetings that share topics with them.
3. Search for `clinic`. Health and wellness events move to the top, while events related
   to the earlier water search move down.
4. Open one of the suggested events. It is removed from the suggestions, and the list
   updates to reflect the topics of the event you opened.

---

## 5. Data structures

Each data structure is used where it makes a real difference to how the page organises or
retrieves information. In .NET, `Dictionary<TKey, TValue>` is the generic hash table.

### 5.1 Stacks, queues and priority queues

| Structure | Where | How it is used |
|---|---|---|
| `Stack<SearchQuery>` | `LocalEventsForm` | **Previous search.** Each new search pushes the search it replaces. Clicking the button pops the most recent one (last in, first out), and `Peek` supplies the tooltip. |
| `Queue<SearchRecord>` | `SearchTracker` | The last 10 searches and what they found. New searches join the back; once there are more than ten, the oldest is removed from the front (first in, first out). |
| `Queue<LocalEvent>` | `SearchTracker` | The last 10 events the resident opened, managed the same way. |
| `PriorityQueue<Announcement, (int, DateTime)>` | `EventManager` | Service notices, ordered by priority (Critical, then High, then Normal) and then by start date. The queue is copied and dequeued for display, so the stored queue is never emptied. |
| `PriorityQueue<Recommendation, double>` | `RecommendationEngine` | A min-heap that keeps only the best three suggestions while every candidate is scored. |

### 5.2 Hash tables, dictionaries and sorted dictionaries

| Structure | Where | How it is used |
|---|---|---|
| `SortedDictionary<DateTime, List<LocalEvent>>` | `EventManager` | The main index: events grouped by calendar day and always kept in date order. Listing events needs no sorting, and a date-range search stops at the first day after the end date. |
| `SortedDictionary<DateTime, List<LocalEvent>>` | `EventManager.Search`, `EventSearchResult` | Search results are grouped by day in date order, ready to display under day headings. |
| `Dictionary<string, List<LocalEvent>>` | `EventManager` | Category index. A category search starts from this list instead of scanning every event. |
| `Dictionary<int, LocalEvent>` | `EventManager` | Finds an event by its ID, and rejects duplicate IDs when events are added. |
| `Dictionary<string, double>` and `Dictionary<DayOfWeek, double>` | `InterestProfile` | Interest weights for categories, keywords, topics and days of the week, used to score suggestions. |
| `Dictionary<string, Color>` | `Theme` | The colour for each category, used on the event cards. |

### 5.3 Sets

| Structure | Where | How it is used |
|---|---|---|
| `HashSet<string>` | `EventManager` | Unique categories across events and notices, used to fill the category list so each category appears exactly once. |
| `HashSet<DateTime>` | `EventManager` | Unique days that have events. Supplies the bold dates on the calendar and answers "is anything on this day?" without searching. |
| `HashSet<string>` | `LocalEvent.Tags` | Each event's topics, case-insensitive and without duplicates. Suggestions use a set intersection to find topics an event shares with the resident's interests. |
| `HashSet<int>` | `EventSearchResult`, `SearchTracker` | The IDs of events on screen and of events already opened, so suggestions can skip them. |
| `HashSet<string>` | `SearchTracker`, `InterestProfile` | Common words ignored in keyword searches, and the categories the resident chose directly. |

---

## 6. Recommendation algorithm

The suggestions use **weighted content-based scoring with top-k selection**.

1. **Interest profile.** `SearchTracker` builds a profile from the queued searches and
   opened events. Each search adds weight to:
   - the category it filtered on;
   - each meaningful word in its keyword;
   - for keyword and category searches, the categories and topics of the events it
     found, shared out across the results;
   - for a search on a single day, that day of the week.

   Each opened event adds weight to its category, its topics and its day of the week.
   Newer activity counts more than older activity: in a queue of *n* items, the newest
   counts fully and the oldest counts 1/*n*.
2. **Candidates.** These are upcoming events that the resident has not opened and that are
   not already in the current results.
3. **Scoring.** Each candidate is scored on four signals, each scaled from 0 to 1:

   | Signal | Weight | Meaning |
   |---|---|---|
   | Category | 3.0 | Interest in the event's category, compared with the strongest category |
   | Keywords | 2.0 | The share of the resident's searched words that the event matches |
   | Topics | 2.0 | Topics the event shares with the profile |
   | Day | 0.5 | Whether the event falls on a day of the week the resident often looks at |

   A small bonus is added for events happening soon, which mostly breaks ties. Events with
   too little in common with the profile are left out.
4. **Top three.** Scored events go into a min-heap of size three; whenever it holds more
   than three, the lowest score is removed. This takes O(n log k) time rather than sorting
   every candidate.
5. **Topping up.** If fewer than three events qualify, the column is filled with the
   soonest remaining events, labelled *Coming up soon*.

The reason shown on each card comes from whichever signal contributed most to its score.

---

## 7. Data storage

All data is held in memory for the duration of the session and is cleared when the
application closes.

- **Reported issues** are stored in a `List<Issue>` managed by the static `IssueManager`
  class. A `List` suits reports because they are added in order and read back as a whole.
- **Events and notices** are sample data created in `Data/SeedData.cs` when the
  application starts: 27 events across nine categories and nine notices, one of
  which has already ended to show that expired notices are hidden. Every date is
  calculated relative to the current date, so the events are always upcoming whenever the
  application is run.
- **Search history** used for suggestions is kept in `SearchTracker` for the session only.

---

## 8. Project structure

```
MunicipalServicesApp/
├── Controls/
│   ├── CardBase.cs               Shared drawing and behaviour for the cards
│   ├── EventCard.cs              An event in the results list
│   ├── AnnouncementCard.cs       A service notice
│   ├── RecommendationCard.cs     A suggested event with its reason
│   └── AutoHeightLabel.cs        Label that grows to fit its text
├── Data/
│   └── SeedData.cs               Sample events and notices, dated relative to today
├── Models/
│   ├── Issue.cs                  A reported issue (Part 1)
│   ├── LocalEvent.cs             A municipal event
│   ├── Announcement.cs           A service notice
│   ├── AnnouncementPriority.cs   Critical, High or Normal
│   ├── EventCategories.cs        Category names
│   ├── SearchQuery.cs            One search: keyword, category and dates
│   ├── EventSearchResult.cs      Search results grouped by day
│   ├── InterestProfile.cs        Interest weights used for suggestions
│   └── Recommendation.cs         A suggested event, its score and its reason
├── Services/
│   ├── IssueManager.cs           Stores reported issues (Part 1)
│   ├── EngagementTracker.cs      Progress feedback for reports (Part 1)
│   ├── EventManager.cs           Event and notice storage, indexes and search
│   ├── SearchTracker.cs          Records searches and opened events
│   └── RecommendationEngine.cs   Scores and selects suggested events
├── UI/
│   └── Theme.cs                  Shared colours, fonts and button styles
├── MainMenuForm.cs               Main menu presented on startup
├── ReportIssuesForm.cs           Issue reporting form (Part 1)
├── LocalEventsForm.cs            Local Events and Announcements page (Part 2)
└── Program.cs                    Application entry point
```

---

## 9. Known limitations

- Reported issues, events and search history are not saved between sessions.
- Events and notices are sample data; there is no screen for municipal staff to add them.
- Attachments are recorded by file path only; the file itself is not copied or embedded.
- Reference numbers restart from `0001` each time the application is launched.
- Service Request Status is intentionally disabled until a later part.

---

## 10. Troubleshooting

**The solution will not build, and the target framework is reported as missing.**
Install the .NET 8.0 SDK, then reopen the solution.

**The designer will not open for a form.**
Build the solution first (F6). The forms style themselves from `Theme.cs` at runtime, so
the project must compile before the designer can load them.

**An attached file cannot be read.**
Confirm the file still exists at its original location and is not open in another
application.

**The Local Events window does not fit on the screen.**
The window opens at 1160 × 660 and can be resized down to 1060 × 640, or maximised.
The event, notice and suggestion columns scroll independently.

**A search returns no events.**
Check that the From and To dates are not too narrow, or choose *All categories*. Days
shown in bold on the calendar have at least one event.

---

## 11. Author

| | |
|---|---|
| Name | Lesego Letsapa |
| Student number | ST10351970 |
| Module | Advanced Application Development (AAPD7112w) |
| Assessment | Part 2, Portfolio of Evidence |
| Repository | https://github.com/ST10351970/XADAD7112 |
