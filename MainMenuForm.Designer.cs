namespace MunicipalServicesApp
{
    partial class MainMenuForm
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
            this.rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.menuLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btnReportIssues = new System.Windows.Forms.Button();
            this.lblReportIssuesInfo = new System.Windows.Forms.Label();
            this.btnLocalEvents = new System.Windows.Forms.Button();
            this.lblLocalEventsInfo = new System.Windows.Forms.Label();
            this.btnServiceStatus = new System.Windows.Forms.Button();
            this.lblServiceStatusInfo = new System.Windows.Forms.Label();
            this.footerLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblIssueCount = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.rootLayout.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.menuLayout.SuspendLayout();
            this.footerLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.ColumnCount = 1;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.headerPanel, 0, 0);
            this.rootLayout.Controls.Add(this.lblInstruction, 0, 1);
            this.rootLayout.Controls.Add(this.menuLayout, 0, 2);
            this.rootLayout.Controls.Add(this.footerLayout, 0, 3);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(0, 0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.RowCount = 4;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.rootLayout.Size = new System.Drawing.Size(784, 611);
            this.rootLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Controls.Add(this.lblSubtitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(784, 96);
            this.headerPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Location = new System.Drawing.Point(28, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(728, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Municipal Services Application";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtitle.Location = new System.Drawing.Point(28, 56);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(728, 24);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Report issues and access municipal services in your community";
            // 
            // lblInstruction
            // 
            this.lblInstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInstruction.Location = new System.Drawing.Point(28, 96);
            this.lblInstruction.Margin = new System.Windows.Forms.Padding(28, 0, 28, 0);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(728, 52);
            this.lblInstruction.TabIndex = 1;
            this.lblInstruction.Text = "Select a service below to get started.";
            this.lblInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuLayout
            // 
            this.menuLayout.ColumnCount = 2;
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.menuLayout.Controls.Add(this.btnReportIssues, 0, 0);
            this.menuLayout.Controls.Add(this.lblReportIssuesInfo, 1, 0);
            this.menuLayout.Controls.Add(this.btnLocalEvents, 0, 1);
            this.menuLayout.Controls.Add(this.lblLocalEventsInfo, 1, 1);
            this.menuLayout.Controls.Add(this.btnServiceStatus, 0, 2);
            this.menuLayout.Controls.Add(this.lblServiceStatusInfo, 1, 2);
            this.menuLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuLayout.Location = new System.Drawing.Point(28, 148);
            this.menuLayout.Margin = new System.Windows.Forms.Padding(28, 0, 28, 0);
            this.menuLayout.Name = "menuLayout";
            this.menuLayout.RowCount = 3;
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.menuLayout.Size = new System.Drawing.Size(728, 407);
            this.menuLayout.TabIndex = 2;
            // 
            // btnReportIssues
            // 
            this.btnReportIssues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReportIssues.Location = new System.Drawing.Point(3, 12);
            this.btnReportIssues.Margin = new System.Windows.Forms.Padding(3, 12, 20, 12);
            this.btnReportIssues.Name = "btnReportIssues";
            this.btnReportIssues.Size = new System.Drawing.Size(237, 111);
            this.btnReportIssues.TabIndex = 0;
            this.btnReportIssues.Text = "Report Issues";
            this.btnReportIssues.UseVisualStyleBackColor = true;
            // 
            // lblReportIssuesInfo
            // 
            this.lblReportIssuesInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReportIssuesInfo.Location = new System.Drawing.Point(263, 0);
            this.lblReportIssuesInfo.Name = "lblReportIssuesInfo";
            this.lblReportIssuesInfo.Size = new System.Drawing.Size(462, 135);
            this.lblReportIssuesInfo.TabIndex = 1;
            this.lblReportIssuesInfo.Text = "Report a problem in your area, such as a water leak, pothole or uncollected refuse" +
    ". You can attach a photograph or document to support your report.";
            this.lblReportIssuesInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLocalEvents
            // 
            this.btnLocalEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLocalEvents.Enabled = false;
            this.btnLocalEvents.Location = new System.Drawing.Point(3, 147);
            this.btnLocalEvents.Margin = new System.Windows.Forms.Padding(3, 12, 20, 12);
            this.btnLocalEvents.Name = "btnLocalEvents";
            this.btnLocalEvents.Size = new System.Drawing.Size(237, 111);
            this.btnLocalEvents.TabIndex = 2;
            this.btnLocalEvents.Text = "Local Events and Announcements";
            this.btnLocalEvents.UseVisualStyleBackColor = true;
            // 
            // lblLocalEventsInfo
            // 
            this.lblLocalEventsInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLocalEventsInfo.Location = new System.Drawing.Point(263, 135);
            this.lblLocalEventsInfo.Name = "lblLocalEventsInfo";
            this.lblLocalEventsInfo.Size = new System.Drawing.Size(462, 135);
            this.lblLocalEventsInfo.TabIndex = 3;
            this.lblLocalEventsInfo.Text = "Not yet available. This service will list municipal events and public announcement" +
    "s in a future release.";
            this.lblLocalEventsInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnServiceStatus
            // 
            this.btnServiceStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnServiceStatus.Enabled = false;
            this.btnServiceStatus.Location = new System.Drawing.Point(3, 282);
            this.btnServiceStatus.Margin = new System.Windows.Forms.Padding(3, 12, 20, 12);
            this.btnServiceStatus.Name = "btnServiceStatus";
            this.btnServiceStatus.Size = new System.Drawing.Size(237, 113);
            this.btnServiceStatus.TabIndex = 4;
            this.btnServiceStatus.Text = "Service Request Status";
            this.btnServiceStatus.UseVisualStyleBackColor = true;
            // 
            // lblServiceStatusInfo
            // 
            this.lblServiceStatusInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceStatusInfo.Location = new System.Drawing.Point(263, 270);
            this.lblServiceStatusInfo.Name = "lblServiceStatusInfo";
            this.lblServiceStatusInfo.Size = new System.Drawing.Size(462, 137);
            this.lblServiceStatusInfo.TabIndex = 5;
            this.lblServiceStatusInfo.Text = "Not yet available. This service will let you track the progress of reports you hav" +
    "e submitted in a future release.";
            this.lblServiceStatusInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // footerLayout
            // 
            this.footerLayout.ColumnCount = 2;
            this.footerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.footerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.footerLayout.Controls.Add(this.lblIssueCount, 0, 0);
            this.footerLayout.Controls.Add(this.btnExit, 1, 0);
            this.footerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.footerLayout.Location = new System.Drawing.Point(28, 555);
            this.footerLayout.Margin = new System.Windows.Forms.Padding(28, 0, 28, 0);
            this.footerLayout.Name = "footerLayout";
            this.footerLayout.RowCount = 1;
            this.footerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.footerLayout.Size = new System.Drawing.Size(728, 56);
            this.footerLayout.TabIndex = 3;
            // 
            // lblIssueCount
            // 
            this.lblIssueCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIssueCount.Location = new System.Drawing.Point(3, 0);
            this.lblIssueCount.Name = "lblIssueCount";
            this.lblIssueCount.Size = new System.Drawing.Size(592, 56);
            this.lblIssueCount.TabIndex = 0;
            this.lblIssueCount.Text = "No issues reported yet.";
            this.lblIssueCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(601, 12);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 12, 3, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(124, 32);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 611);
            this.Controls.Add(this.rootLayout);
            this.MinimumSize = new System.Drawing.Size(700, 560);
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Municipal Services Application - Main Menu";
            this.rootLayout.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.menuLayout.ResumeLayout(false);
            this.footerLayout.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.TableLayoutPanel menuLayout;
        private System.Windows.Forms.Button btnReportIssues;
        private System.Windows.Forms.Label lblReportIssuesInfo;
        private System.Windows.Forms.Button btnLocalEvents;
        private System.Windows.Forms.Label lblLocalEventsInfo;
        private System.Windows.Forms.Button btnServiceStatus;
        private System.Windows.Forms.Label lblServiceStatusInfo;
        private System.Windows.Forms.TableLayoutPanel footerLayout;
        private System.Windows.Forms.Label lblIssueCount;
        private System.Windows.Forms.Button btnExit;
    }
}