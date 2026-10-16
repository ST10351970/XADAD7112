using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MunicipalServicesApp.Controls;
using MunicipalServicesApp.Models;
using MunicipalServicesApp.Services;
using MunicipalServicesApp.UI;

namespace MunicipalServicesApp
{
    /// <summary>
    /// Lists upcoming local events grouped by day, shows active service notices
    /// in priority order, lets residents search by keyword, category and date, and
    /// suggests events based on what they search for and open.
    /// </summary>
    public partial class LocalEventsForm : Form
    {
        private const string AllCategoriesOption = "All categories";
        private const int SuggestionCount = 3;

        private readonly EventManager _eventManager;
        private readonly SearchTracker _tracker;
        private readonly RecommendationEngine _recommender;

        // Searches the resident can return to with "Previous search". A stack gives
        // last-in, first-out order: each click goes back one step, like a browser.
        private readonly Stack<SearchQuery> _previousSearches = new Stack<SearchQuery>();

        private SearchQuery _currentQuery = SearchQuery.Empty;
        private EventSearchResult? _currentResult;

        public LocalEventsForm() : this(EventManager.Shared, SearchTracker.Shared)
        {
        }

        public LocalEventsForm(EventManager eventManager, SearchTracker tracker)
        {
            _eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
            _tracker = tracker ?? throw new ArgumentNullException(nameof(tracker));
            _recommender = new RecommendationEngine(_eventManager, _tracker);

            InitializeComponent();
            ApplyTheme();
            ConfigureFilters();
            WireUpEvents();
            RunSearch(SearchQuery.Empty, false);
        }

        // ------------------------------------------------------------------
        // Setup
        // ------------------------------------------------------------------

        private void ApplyTheme()
        {
            BackColor = Theme.Surface;

            headerPanel.BackColor = Theme.Primary;
            lblFormTitle.Font = Theme.TitleFont;
            lblFormTitle.ForeColor = Theme.TextOnPrimary;
            lblFormSubtitle.Font = Theme.SubtitleFont;
            lblFormSubtitle.ForeColor = Theme.TextOnPrimary;

            foreach (Label label in new[] { lblKeyword, lblCategory, lblFrom, lblTo })
            {
                label.Font = Theme.SmallFont;
                label.ForeColor = Theme.TextMuted;
            }

            txtKeyword.Font = Theme.BodyFont;
            cboCategory.Font = Theme.BodyFont;
            dtpFrom.Font = Theme.BodyFont;
            dtpTo.Font = Theme.BodyFont;

            Theme.StyleActionButton(btnSearch);
            Theme.StyleSecondaryButton(btnClear);
            Theme.StyleDisabledButton(btnPrevious);
            Theme.StyleSecondaryButton(btnBackToMenu);

            foreach (Label heading in new[] { lblNoticesHeading, lblResultsHeading, lblSuggestionsHeading })
            {
                heading.Font = Theme.SectionFont;
                heading.ForeColor = Theme.TextPrimary;
            }

            foreach (FlowLayoutPanel panel in new[] { flowNotices, flowResults, flowSuggestions })
            {
                panel.BackColor = Theme.Surface;
            }

            lblStatus.Font = Theme.SmallFont;
            lblStatus.ForeColor = Theme.TextMuted;
        }

        private void ConfigureFilters()
        {
            // The category list comes from the HashSet of unique categories held by
            // EventManager, so every category appears exactly once.
            cboCategory.Items.Clear();
            cboCategory.Items.Add(AllCategoriesOption);
            foreach (string category in _eventManager.GetCategories())
            {
                cboCategory.Items.Add(category);
            }
            cboCategory.SelectedIndex = 0;

            DateTime today = DateTime.Today;

            // Value is set before MinDate so it is never outside the allowed range.
            // Checked must be set after Value, because setting Value ticks the box.
            dtpFrom.Value = today;
            dtpFrom.MinDate = today;
            dtpFrom.Checked = false;

            dtpTo.Value = today.AddDays(14);
            dtpTo.MinDate = today;
            dtpTo.Checked = false;

            // Days that have events are shown in bold, taken from the HashSet of event dates.
            calendarEvents.MinDate = today;
            calendarEvents.BoldedDates = _eventManager.GetEventDates();

            toolTip.SetToolTip(txtKeyword, "Search titles, descriptions, venues, areas and tags. Separate words with spaces.");
            toolTip.SetToolTip(dtpFrom, "Tick the box to only show events on or after this date.");
            toolTip.SetToolTip(dtpTo, "Tick the box to only show events on or before this date.");
            toolTip.SetToolTip(calendarEvents, "Days in bold have events. Click a day to see what is on.");
            toolTip.SetToolTip(btnClear, "Remove all filters and show every upcoming event.");
        }

        private void WireUpEvents()
        {
            btnSearch.Click += BtnSearch_Click;
            btnClear.Click += BtnClear_Click;
            btnPrevious.Click += BtnPrevious_Click;
            btnBackToMenu.Click += BtnBackToMenu_Click;
            calendarEvents.DateSelected += CalendarEvents_DateSelected;

            // Cards are resized to the panel width whenever the window (or a scroll bar) changes it.
            flowNotices.ClientSizeChanged += FlowPanel_ClientSizeChanged;
            flowResults.ClientSizeChanged += FlowPanel_ClientSizeChanged;
            flowSuggestions.ClientSizeChanged += FlowPanel_ClientSizeChanged;

            // Enter in the keyword box runs the search; Escape returns to the main menu.
            AcceptButton = btnSearch;
            CancelButton = btnBackToMenu;
        }

        // ------------------------------------------------------------------
        // Event handlers
        // ------------------------------------------------------------------

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            SearchQuery? query = ReadQueryFromFilters();
            if (query != null)
            {
                RunSearch(query, true);
            }
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            ShowQueryInFilters(SearchQuery.Empty);
            RunSearch(SearchQuery.Empty, true);
            txtKeyword.Focus();
        }

        private void BtnPrevious_Click(object? sender, EventArgs e)
        {
            if (_previousSearches.Count == 0) return;

            SearchQuery previous = _previousSearches.Pop();
            ShowQueryInFilters(previous);
            RunSearch(previous, false);
        }

        private void BtnBackToMenu_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void CalendarEvents_DateSelected(object? sender, DateRangeEventArgs e)
        {
            // Clicking a day shows everything on that day, whatever the other filters were.
            DateTime day = e.Start.Date;
            SearchQuery query = new SearchQuery(null, null, day, day);

            ShowQueryInFilters(query);
            RunSearch(query, true);
        }

        private void Card_EventSelected(object? sender, LocalEvent localEvent)
        {
            // Opening an event is the strongest signal of interest, so record it
            // and refresh the suggestions straight away.
            _tracker.RecordView(localEvent);
            ShowEventDetails(localEvent);

            // The card that was clicked may itself be in the suggestions panel, so the
            // refresh waits until its click has finished instead of disposing it mid-click.
            BeginInvoke(new Action(RefreshSuggestions));
        }

        private void RefreshSuggestions()
        {
            if (IsDisposed || _currentResult == null) return;

            ShowSuggestions(_currentResult);
        }

        private void FlowPanel_ClientSizeChanged(object? sender, EventArgs e)
        {
            if (sender is FlowLayoutPanel panel)
            {
                FitChildrenToWidth(panel);
            }
        }

        // ------------------------------------------------------------------
        // Searching
        // ------------------------------------------------------------------

        /// <summary>
        /// Builds a query from the filter controls, or returns null and explains
        /// the problem if the dates are the wrong way round.
        /// </summary>
        private SearchQuery? ReadQueryFromFilters()
        {
            DateTime? fromDate = dtpFrom.Checked ? dtpFrom.Value.Date : (DateTime?)null;
            DateTime? toDate = dtpTo.Checked ? dtpTo.Value.Date : (DateTime?)null;

            if (fromDate.HasValue && toDate.HasValue && fromDate.Value > toDate.Value)
            {
                MessageBox.Show(
                    "The 'From' date must be on or before the 'To' date. Please adjust the dates and search again.",
                    "Check the dates",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                dtpTo.Focus();
                return null;
            }

            string? category = null;
            if (cboCategory.SelectedIndex > 0)
            {
                category = cboCategory.SelectedItem as string;
            }

            return new SearchQuery(txtKeyword.Text, category, fromDate, toDate);
        }

        /// <summary>
        /// Puts a query's values back into the filter controls, used by Clear,
        /// Previous search and the calendar so the filters always match the results.
        /// </summary>
        private void ShowQueryInFilters(SearchQuery query)
        {
            txtKeyword.Text = query.Keyword;

            int categoryIndex = 0;
            if (query.Category != null)
            {
                int index = cboCategory.Items.IndexOf(query.Category);
                if (index > 0) categoryIndex = index;
            }
            cboCategory.SelectedIndex = categoryIndex;

            SetPicker(dtpFrom, query.FromDate);
            SetPicker(dtpTo, query.ToDate);
        }

        private static void SetPicker(DateTimePicker picker, DateTime? value)
        {
            if (value.HasValue)
            {
                DateTime date = value.Value;
                if (date < picker.MinDate) date = picker.MinDate;
                if (date > picker.MaxDate) date = picker.MaxDate;

                picker.Value = date;
                picker.Checked = true;
            }
            else
            {
                picker.Checked = false;
            }
        }

        /// <summary>
        /// Runs a search and refreshes every panel. When rememberCurrent is true the
        /// search being replaced is pushed onto the Previous search stack first.
        /// </summary>
        private void RunSearch(SearchQuery query, bool rememberCurrent)
        {
            // A new search (not a repeat, and not a step back with Previous search)
            // goes on the Back stack and into the search history used for suggestions.
            bool isNewSearch = rememberCurrent && !query.IsSameSearchAs(_currentQuery);

            if (isNewSearch)
            {
                _previousSearches.Push(_currentQuery);
            }

            _currentQuery = query;

            EventSearchResult result = _eventManager.Search(query);
            _currentResult = result;

            if (isNewSearch)
            {
                _tracker.RecordSearch(query, result);
            }

            ShowResults(result);
            ShowNotices(query.Category);
            ShowSuggestions(result);
            UpdatePreviousButton();
            UpdateStatus(result);
        }

        // ------------------------------------------------------------------
        // Displaying results
        // ------------------------------------------------------------------

        private void ShowResults(EventSearchResult result)
        {
            lblResultsHeading.Text = string.Format("{0}  ({1})",
                result.Query.Describe(), CountText(result.TotalCount, "event", "events"));

            flowResults.SuspendLayout();
            ClearPanel(flowResults);

            if (result.IsEmpty)
            {
                flowResults.Controls.Add(CreateMessageLabel(GetNoResultsMessage(result.Query)));
            }
            else
            {
                // The result is a SortedDictionary keyed by day, so the days come out
                // in date order and each day's events are already in time order.
                bool firstDay = true;
                foreach (KeyValuePair<DateTime, List<LocalEvent>> day in result.EventsByDate)
                {
                    flowResults.Controls.Add(CreateDayHeading(day.Key, day.Value.Count, firstDay));
                    firstDay = false;

                    foreach (LocalEvent localEvent in day.Value)
                    {
                        EventCard card = new EventCard(localEvent);
                        card.EventSelected += Card_EventSelected;
                        flowResults.Controls.Add(card);
                    }
                }
            }

            FitChildrenToWidth(flowResults);
            flowResults.ResumeLayout(true);
            flowResults.AutoScrollPosition = new Point(0, 0);
        }

        private string GetNoResultsMessage(SearchQuery query)
        {
            // A single-day search on a day with no events: point the resident at the bold days.
            if (query.FromDate.HasValue && query.FromDate == query.ToDate &&
                !_eventManager.HasEventsOn(query.FromDate.Value))
            {
                return string.Format(
                    "Nothing is scheduled for {0:dddd d MMMM}. Days shown in bold on the calendar have events.",
                    query.FromDate.Value);
            }

            // A category that only has notices, such as General Notices.
            if (query.Category != null && _eventManager.GetEventCountForCategory(query.Category) == 0)
            {
                return string.Format(
                    "There are no events in {0} at the moment. Any related service notices are listed on the left.",
                    query.Category);
            }

            return "No events match this search. Try a different keyword, choose All categories or widen the dates.";
        }

        private void ShowNotices(string? category)
        {
            List<Announcement> notices = _eventManager.GetActiveAnnouncements(DateTime.Today, category);

            lblNoticesHeading.Text = category == null ? "Service notices" : "Notices: " + category;

            flowNotices.SuspendLayout();
            ClearPanel(flowNotices);

            if (notices.Count == 0)
            {
                flowNotices.Controls.Add(CreateMessageLabel(category == null
                    ? "There are no active service notices."
                    : "There are no active notices for this category."));
            }
            else
            {
                // Already in priority order: the list was produced by dequeuing a priority queue.
                foreach (Announcement notice in notices)
                {
                    flowNotices.Controls.Add(new AnnouncementCard(notice));
                }
            }

            FitChildrenToWidth(flowNotices);
            flowNotices.ResumeLayout(true);
            flowNotices.AutoScrollPosition = new Point(0, 0);
        }

        /// <summary>
        /// Fills the suggestions column from the recommendation engine. Before the
        /// resident has searched or opened anything, it shows the soonest events.
        /// </summary>
        private void ShowSuggestions(EventSearchResult result)
        {
            List<Recommendation> suggestions =
                _recommender.GetRecommendations(result, DateTime.Today, SuggestionCount);

            bool personalised = false;
            foreach (Recommendation suggestion in suggestions)
            {
                if (suggestion.IsPersonalised)
                {
                    personalised = true;
                    break;
                }
            }

            lblSuggestionsHeading.Text = personalised ? "Recommended for you" : "Coming up soon";
            toolTip.SetToolTip(lblSuggestionsHeading, DescribeRecentSearches());

            flowSuggestions.SuspendLayout();
            ClearPanel(flowSuggestions);

            flowSuggestions.Controls.Add(CreateNoteLabel(DescribeSuggestionBasis(personalised)));

            foreach (Recommendation suggestion in suggestions)
            {
                RecommendationCard card = new RecommendationCard(suggestion.Event, suggestion.Reason);
                card.EventSelected += Card_EventSelected;
                flowSuggestions.Controls.Add(card);
            }

            FitChildrenToWidth(flowSuggestions);
            flowSuggestions.ResumeLayout(true);
            flowSuggestions.AutoScrollPosition = new Point(0, 0);
        }

        // Explains what the suggestions are based on, including the resident's top
        // interests, so the feature does not feel like a black box.
        private string DescribeSuggestionBasis(bool personalised)
        {
            if (!_tracker.HasHistory)
            {
                return "Search for events or open one to get suggestions tailored to you.";
            }

            List<string> parts = new List<string>();
            if (_tracker.RecentSearchCount > 0)
            {
                parts.Add("your last " + CountText(_tracker.RecentSearchCount, "search", "searches"));
            }
            if (_tracker.ViewCount > 0)
            {
                parts.Add(CountText(_tracker.ViewCount, "event you opened", "events you opened"));
            }

            string basis = parts.Count > 0
                ? "Based on " + string.Join(" and ", parts) + "."
                : "Based on your activity.";

            List<string> topInterests = _tracker.BuildProfile().GetTopCategories(2);
            if (topInterests.Count > 0)
            {
                basis += " Top interests: " + string.Join(", ", topInterests) + ".";
            }

            if (!personalised)
            {
                basis += " Nothing new matches yet, so here is what is coming up soon.";
            }

            return basis;
        }

        // Tooltip on the suggestions heading: recent searches, newest first.
        private string DescribeRecentSearches()
        {
            List<SearchQuery> recent = _tracker.GetRecentQueries();
            if (recent.Count == 0)
            {
                return "No searches yet.";
            }

            List<string> lines = new List<string>();
            foreach (SearchQuery query in recent)
            {
                lines.Add(query.Describe());
            }
            lines.Reverse();

            return "Recent searches (newest first):\n" + string.Join("\n", lines);
        }

        private void UpdatePreviousButton()
        {
            if (_previousSearches.Count > 0)
            {
                Theme.StyleSecondaryButton(btnPrevious);
                btnPrevious.Enabled = true;
                toolTip.SetToolTip(btnPrevious, "Go back to: " + _previousSearches.Peek().Describe());
            }
            else
            {
                Theme.StyleDisabledButton(btnPrevious);
                toolTip.SetToolTip(btnPrevious, string.Empty);
            }
        }

        private void UpdateStatus(EventSearchResult result)
        {
            int activeNotices = _eventManager.GetActiveAnnouncements(DateTime.Today).Count;

            lblStatus.Text = string.Format(
                "Showing {0} of {1} upcoming events across {2} days  |  {3}  |  Click an event for full details",
                result.TotalCount,
                _eventManager.EventCount,
                result.DayCount,
                CountText(activeNotices, "active service notice", "active service notices"));
        }

        private void ShowEventDetails(LocalEvent localEvent)
        {
            string details = string.Format(
                "{0}\n\nCategory: {1}\nWhen: {2:dddd d MMMM yyyy}, {3}\nWhere: {4}\n\n{5}",
                localEvent.Title,
                localEvent.Category,
                localEvent.StartDateTime,
                localEvent.TimeRangeText,
                localEvent.LocationText,
                localEvent.Description);

            if (localEvent.Tags.Count > 0)
            {
                details += "\n\nTopics: " + string.Join(", ", localEvent.Tags);
            }

            MessageBox.Show(details, "Event details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ------------------------------------------------------------------
        // Small layout helpers
        // ------------------------------------------------------------------

        private static void ClearPanel(FlowLayoutPanel panel)
        {
            // Dispose each control so old cards do not stay in memory; disposing
            // a control also removes it from its parent's collection.
            for (int i = panel.Controls.Count - 1; i >= 0; i--)
            {
                panel.Controls[i].Dispose();
            }
        }

        private static void FitChildrenToWidth(FlowLayoutPanel panel)
        {
            int available = panel.ClientSize.Width - panel.Padding.Horizontal - 2;
            if (available < 80) return;

            panel.SuspendLayout();
            foreach (Control child in panel.Controls)
            {
                int width = available - child.Margin.Horizontal;
                if (child.Width != width)
                {
                    child.Width = width;
                }
            }
            panel.ResumeLayout(true);
        }

        private static Label CreateDayHeading(DateTime day, int eventCount, bool isFirst)
        {
            return new Label
            {
                AutoSize = false,
                Height = isFirst ? 28 : 40,
                Margin = new Padding(0),
                Text = DescribeDay(day) + "  |  " + CountText(eventCount, "event", "events"),
                TextAlign = ContentAlignment.BottomLeft,
                Padding = new Padding(0, 0, 0, 6),
                Font = Theme.SmallBoldFont,
                ForeColor = Theme.Primary,
                UseMnemonic = false
            };
        }

        private static Label CreateMessageLabel(string message)
        {
            return new AutoHeightLabel
            {
                Margin = new Padding(0, 4, 0, 0),
                Text = message,
                TextAlign = ContentAlignment.TopLeft,
                Font = Theme.BodyFont,
                ForeColor = Theme.TextMuted
            };
        }

        private static Label CreateNoteLabel(string note)
        {
            return new AutoHeightLabel
            {
                Margin = new Padding(0, 0, 0, 8),
                Text = note,
                TextAlign = ContentAlignment.TopLeft,
                Font = Theme.SmallFont,
                ForeColor = Theme.TextMuted
            };
        }

        private static string DescribeDay(DateTime day)
        {
            DateTime today = DateTime.Today;

            if (day == today) return "Today, " + day.ToString("dddd d MMMM");
            if (day == today.AddDays(1)) return "Tomorrow, " + day.ToString("dddd d MMMM");

            return day.ToString("dddd d MMMM");
        }

        private static string CountText(int count, string singular, string plural)
        {
            return count + " " + (count == 1 ? singular : plural);
        }
    }
}
