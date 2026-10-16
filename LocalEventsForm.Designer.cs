namespace MunicipalServicesApp
{
    partial class LocalEventsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblFormSubtitle = new System.Windows.Forms.Label();
            this.filterLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this.noticesLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblNoticesHeading = new System.Windows.Forms.Label();
            this.flowNotices = new System.Windows.Forms.FlowLayoutPanel();
            this.resultsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblResultsHeading = new System.Windows.Forms.Label();
            this.flowResults = new System.Windows.Forms.FlowLayoutPanel();
            this.sideLayout = new System.Windows.Forms.TableLayoutPanel();
            this.calendarEvents = new System.Windows.Forms.MonthCalendar();
            this.lblSuggestionsHeading = new System.Windows.Forms.Label();
            this.flowSuggestions = new System.Windows.Forms.FlowLayoutPanel();
            this.footerLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnBackToMenu = new System.Windows.Forms.Button();
            this.rootLayout.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.filterLayout.SuspendLayout();
            this.contentLayout.SuspendLayout();
            this.noticesLayout.SuspendLayout();
            this.resultsLayout.SuspendLayout();
            this.sideLayout.SuspendLayout();
            this.footerLayout.SuspendLayout();
            this.SuspendLayout();
            //
            // rootLayout
            //
            this.rootLayout.ColumnCount = 1;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.headerPanel, 0, 0);
            this.rootLayout.Controls.Add(this.filterLayout, 0, 1);
            this.rootLayout.Controls.Add(this.contentLayout, 0, 2);
            this.rootLayout.Controls.Add(this.footerLayout, 0, 3);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(0, 0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.RowCount = 4;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.rootLayout.Size = new System.Drawing.Size(1160, 660);
            this.rootLayout.TabIndex = 0;
            //
            // headerPanel
            //
            this.headerPanel.Controls.Add(this.lblFormTitle);
            this.headerPanel.Controls.Add(this.lblFormSubtitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1160, 72);
            this.headerPanel.TabIndex = 0;
            //
            // lblFormTitle
            //
            this.lblFormTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFormTitle.Location = new System.Drawing.Point(28, 10);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(1104, 32);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Local Events and Announcements";
            //
            // lblFormSubtitle
            //
            this.lblFormSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFormSubtitle.Location = new System.Drawing.Point(28, 42);
            this.lblFormSubtitle.Name = "lblFormSubtitle";
            this.lblFormSubtitle.Size = new System.Drawing.Size(1104, 22);
            this.lblFormSubtitle.TabIndex = 1;
            this.lblFormSubtitle.Text = "See what is happening in your community and stay up to date with service notices";
            //
            // filterLayout
            //
            this.filterLayout.ColumnCount = 7;
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.filterLayout.Controls.Add(this.lblKeyword, 0, 0);
            this.filterLayout.Controls.Add(this.lblCategory, 1, 0);
            this.filterLayout.Controls.Add(this.lblFrom, 2, 0);
            this.filterLayout.Controls.Add(this.lblTo, 3, 0);
            this.filterLayout.Controls.Add(this.txtKeyword, 0, 1);
            this.filterLayout.Controls.Add(this.cboCategory, 1, 1);
            this.filterLayout.Controls.Add(this.dtpFrom, 2, 1);
            this.filterLayout.Controls.Add(this.dtpTo, 3, 1);
            this.filterLayout.Controls.Add(this.btnSearch, 4, 1);
            this.filterLayout.Controls.Add(this.btnClear, 5, 1);
            this.filterLayout.Controls.Add(this.btnPrevious, 6, 1);
            this.filterLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterLayout.Location = new System.Drawing.Point(28, 80);
            this.filterLayout.Margin = new System.Windows.Forms.Padding(28, 8, 28, 6);
            this.filterLayout.Name = "filterLayout";
            this.filterLayout.RowCount = 2;
            this.filterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.filterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filterLayout.Size = new System.Drawing.Size(1104, 66);
            this.filterLayout.TabIndex = 1;
            //
            // lblKeyword
            //
            this.lblKeyword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.TabIndex = 0;
            this.lblKeyword.Text = "Keyword";
            this.lblKeyword.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // lblCategory
            //
            this.lblCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Category";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // lblFrom
            //
            this.lblFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Text = "From (tick to use)";
            this.lblFrom.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // lblTo
            //
            this.lblTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTo.Name = "lblTo";
            this.lblTo.TabIndex = 3;
            this.lblTo.Text = "To (tick to use)";
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // txtKeyword
            //
            this.txtKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyword.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.txtKeyword.MaxLength = 100;
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.PlaceholderText = "e.g. clinic, water, youth";
            this.txtKeyword.Size = new System.Drawing.Size(200, 25);
            this.txtKeyword.TabIndex = 4;
            //
            // cboCategory
            //
            this.cboCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(160, 25);
            this.cboCategory.TabIndex = 5;
            //
            // dtpFrom
            //
            this.dtpFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFrom.CustomFormat = "dd MMM yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowCheckBox = true;
            this.dtpFrom.Size = new System.Drawing.Size(137, 25);
            this.dtpFrom.TabIndex = 6;
            //
            // dtpTo
            //
            this.dtpTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpTo.CustomFormat = "dd MMM yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowCheckBox = true;
            this.dtpTo.Size = new System.Drawing.Size(137, 25);
            this.dtpTo.TabIndex = 7;
            //
            // btnSearch
            //
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            //
            // btnClear
            //
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            //
            // btnPrevious
            //
            this.btnPrevious.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(3, 4, 0, 6);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.TabIndex = 10;
            this.btnPrevious.Text = "Previous search";
            this.btnPrevious.UseVisualStyleBackColor = true;
            //
            // contentLayout
            //
            this.contentLayout.ColumnCount = 3;
            this.contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 284F));
            this.contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 262F));
            this.contentLayout.Controls.Add(this.noticesLayout, 0, 0);
            this.contentLayout.Controls.Add(this.resultsLayout, 1, 0);
            this.contentLayout.Controls.Add(this.sideLayout, 2, 0);
            this.contentLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentLayout.Location = new System.Drawing.Point(28, 156);
            this.contentLayout.Margin = new System.Windows.Forms.Padding(28, 4, 28, 0);
            this.contentLayout.Name = "contentLayout";
            this.contentLayout.RowCount = 1;
            this.contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contentLayout.Size = new System.Drawing.Size(1104, 452);
            this.contentLayout.TabIndex = 2;
            //
            // noticesLayout
            //
            this.noticesLayout.ColumnCount = 1;
            this.noticesLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.noticesLayout.Controls.Add(this.lblNoticesHeading, 0, 0);
            this.noticesLayout.Controls.Add(this.flowNotices, 0, 1);
            this.noticesLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noticesLayout.Margin = new System.Windows.Forms.Padding(0);
            this.noticesLayout.Name = "noticesLayout";
            this.noticesLayout.RowCount = 2;
            this.noticesLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.noticesLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.noticesLayout.TabIndex = 0;
            //
            // lblNoticesHeading
            //
            this.lblNoticesHeading.AutoEllipsis = true;
            this.lblNoticesHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoticesHeading.Margin = new System.Windows.Forms.Padding(0);
            this.lblNoticesHeading.Name = "lblNoticesHeading";
            this.lblNoticesHeading.TabIndex = 0;
            this.lblNoticesHeading.Text = "Service notices";
            this.lblNoticesHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNoticesHeading.UseMnemonic = false;
            //
            // flowNotices
            //
            this.flowNotices.AutoScroll = true;
            this.flowNotices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowNotices.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowNotices.Margin = new System.Windows.Forms.Padding(0);
            this.flowNotices.Name = "flowNotices";
            this.flowNotices.TabIndex = 1;
            this.flowNotices.WrapContents = false;
            //
            // resultsLayout
            //
            this.resultsLayout.ColumnCount = 1;
            this.resultsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.resultsLayout.Controls.Add(this.lblResultsHeading, 0, 0);
            this.resultsLayout.Controls.Add(this.flowResults, 0, 1);
            this.resultsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsLayout.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.resultsLayout.Name = "resultsLayout";
            this.resultsLayout.RowCount = 2;
            this.resultsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.resultsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.resultsLayout.TabIndex = 1;
            //
            // lblResultsHeading
            //
            this.lblResultsHeading.AutoEllipsis = true;
            this.lblResultsHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResultsHeading.Margin = new System.Windows.Forms.Padding(0);
            this.lblResultsHeading.Name = "lblResultsHeading";
            this.lblResultsHeading.TabIndex = 0;
            this.lblResultsHeading.Text = "Upcoming events";
            this.lblResultsHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblResultsHeading.UseMnemonic = false;
            //
            // flowResults
            //
            this.flowResults.AutoScroll = true;
            this.flowResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowResults.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowResults.Margin = new System.Windows.Forms.Padding(0);
            this.flowResults.Name = "flowResults";
            this.flowResults.TabIndex = 1;
            this.flowResults.WrapContents = false;
            //
            // sideLayout
            //
            this.sideLayout.ColumnCount = 1;
            this.sideLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sideLayout.Controls.Add(this.calendarEvents, 0, 0);
            this.sideLayout.Controls.Add(this.lblSuggestionsHeading, 0, 1);
            this.sideLayout.Controls.Add(this.flowSuggestions, 0, 2);
            this.sideLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideLayout.Margin = new System.Windows.Forms.Padding(0);
            this.sideLayout.Name = "sideLayout";
            this.sideLayout.RowCount = 3;
            this.sideLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 178F));
            this.sideLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.sideLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sideLayout.TabIndex = 2;
            //
            // calendarEvents
            //
            this.calendarEvents.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.calendarEvents.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.calendarEvents.MaxSelectionCount = 1;
            this.calendarEvents.Name = "calendarEvents";
            this.calendarEvents.TabIndex = 0;
            //
            // lblSuggestionsHeading
            //
            this.lblSuggestionsHeading.AutoEllipsis = true;
            this.lblSuggestionsHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSuggestionsHeading.Margin = new System.Windows.Forms.Padding(0);
            this.lblSuggestionsHeading.Name = "lblSuggestionsHeading";
            this.lblSuggestionsHeading.TabIndex = 1;
            this.lblSuggestionsHeading.Text = "Coming up soon";
            this.lblSuggestionsHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSuggestionsHeading.UseMnemonic = false;
            //
            // flowSuggestions
            //
            this.flowSuggestions.AutoScroll = true;
            this.flowSuggestions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowSuggestions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowSuggestions.Margin = new System.Windows.Forms.Padding(0);
            this.flowSuggestions.Name = "flowSuggestions";
            this.flowSuggestions.TabIndex = 2;
            this.flowSuggestions.WrapContents = false;
            //
            // footerLayout
            //
            this.footerLayout.ColumnCount = 2;
            this.footerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.footerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.footerLayout.Controls.Add(this.lblStatus, 0, 0);
            this.footerLayout.Controls.Add(this.btnBackToMenu, 1, 0);
            this.footerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.footerLayout.Location = new System.Drawing.Point(28, 608);
            this.footerLayout.Margin = new System.Windows.Forms.Padding(28, 0, 28, 0);
            this.footerLayout.Name = "footerLayout";
            this.footerLayout.RowCount = 1;
            this.footerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.footerLayout.Size = new System.Drawing.Size(1104, 52);
            this.footerLayout.TabIndex = 3;
            //
            // lblStatus
            //
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.UseMnemonic = false;
            //
            // btnBackToMenu
            //
            this.btnBackToMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBackToMenu.Margin = new System.Windows.Forms.Padding(3, 10, 0, 10);
            this.btnBackToMenu.Name = "btnBackToMenu";
            this.btnBackToMenu.TabIndex = 1;
            this.btnBackToMenu.Text = "Back to Main Menu";
            this.btnBackToMenu.UseVisualStyleBackColor = true;
            //
            // LocalEventsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 660);
            this.Controls.Add(this.rootLayout);
            this.MinimumSize = new System.Drawing.Size(1060, 640);
            this.Name = "LocalEventsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Municipal Services Application - Local Events and Announcements";
            this.rootLayout.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.filterLayout.ResumeLayout(false);
            this.filterLayout.PerformLayout();
            this.contentLayout.ResumeLayout(false);
            this.noticesLayout.ResumeLayout(false);
            this.resultsLayout.ResumeLayout(false);
            this.sideLayout.ResumeLayout(false);
            this.footerLayout.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblFormSubtitle;
        private System.Windows.Forms.TableLayoutPanel filterLayout;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.TableLayoutPanel contentLayout;
        private System.Windows.Forms.TableLayoutPanel noticesLayout;
        private System.Windows.Forms.Label lblNoticesHeading;
        private System.Windows.Forms.FlowLayoutPanel flowNotices;
        private System.Windows.Forms.TableLayoutPanel resultsLayout;
        private System.Windows.Forms.Label lblResultsHeading;
        private System.Windows.Forms.FlowLayoutPanel flowResults;
        private System.Windows.Forms.TableLayoutPanel sideLayout;
        private System.Windows.Forms.MonthCalendar calendarEvents;
        private System.Windows.Forms.Label lblSuggestionsHeading;
        private System.Windows.Forms.FlowLayoutPanel flowSuggestions;
        private System.Windows.Forms.TableLayoutPanel footerLayout;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnBackToMenu;
    }
}
