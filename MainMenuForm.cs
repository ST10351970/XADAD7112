using System;
using System.Windows.Forms;
using MunicipalServicesApp.Services;
using MunicipalServicesApp.UI;

namespace MunicipalServicesApp
{
    //application's main menu
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
            ApplyTheme();
            WireUpEvents();
            RefreshIssueCount();
        }

        private void ApplyTheme()
        {
            BackColor = Theme.Surface;

            headerPanel.BackColor = Theme.Primary;
            lblTitle.Font = Theme.TitleFont;
            lblTitle.ForeColor = Theme.TextOnPrimary;
            lblSubtitle.Font = Theme.SubtitleFont;
            lblSubtitle.ForeColor = Theme.TextOnPrimary;

            lblInstruction.Font = Theme.SectionFont;
            lblInstruction.ForeColor = Theme.TextPrimary;

            Theme.StyleActionButton(btnReportIssues);
            Theme.StyleDisabledButton(btnLocalEvents);
            Theme.StyleDisabledButton(btnServiceStatus);
            Theme.StyleSecondaryButton(btnExit);

            lblReportIssuesInfo.Font = Theme.BodyFont;
            lblReportIssuesInfo.ForeColor = Theme.TextPrimary;
            lblLocalEventsInfo.Font = Theme.BodyFont;
            lblLocalEventsInfo.ForeColor = Theme.TextMuted;
            lblServiceStatusInfo.Font = Theme.BodyFont;
            lblServiceStatusInfo.ForeColor = Theme.TextMuted;

            lblIssueCount.Font = Theme.SmallFont;
            lblIssueCount.ForeColor = Theme.TextMuted;
        }

        private void WireUpEvents()
        {
            btnReportIssues.Click += BtnReportIssues_Click;
            btnExit.Click += BtnExit_Click;
            Activated += MainMenuForm_Activated;
        }

        private void BtnReportIssues_Click(object sender, EventArgs e)
        {
            using (ReportIssuesForm reportForm = new ReportIssuesForm())
            {
                Hide();
                reportForm.ShowDialog(this);
                Show();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to close the application?",
                "Confirm exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MainMenuForm_Activated(object sender, EventArgs e)
        {
            RefreshIssueCount();
        }

        private void RefreshIssueCount()
        {
            int count = IssueManager.Count;

            if (count == 0)
            {
                lblIssueCount.Text = "No issues reported yet. Your reports will be listed here.";
            }
            else if (count == 1)
            {
                lblIssueCount.Text = "1 issue reported in this session. Thank you for contributing.";
            }
            else
            {
                lblIssueCount.Text = count + " issues reported in this session. Thank you for contributing.";
            }
        }
    }
}