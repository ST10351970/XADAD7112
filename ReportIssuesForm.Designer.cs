namespace MunicipalServicesApp
{
    partial class ReportIssuesForm
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
            rootLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            lblFormTitle = new Label();
            lblFormSubtitle = new Label();
            progressLayout = new TableLayoutPanel();
            lblEngagement = new Label();
            prgReport = new ProgressBar();
            formLayout = new TableLayoutPanel();
            lblLocation = new Label();
            txtLocation = new TextBox();
            lblCategory = new Label();
            cboCategory = new ComboBox();
            lblDescription = new Label();
            rtbDescription = new RichTextBox();
            lblAttachment = new Label();
            attachmentLayout = new TableLayoutPanel();
            btnAttach = new Button();
            lblAttachmentName = new Label();
            footerLayout = new TableLayoutPanel();
            btnBack = new Button();
            btnClear = new Button();
            btnSubmit = new Button();
            rootLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            progressLayout.SuspendLayout();
            formLayout.SuspendLayout();
            attachmentLayout.SuspendLayout();
            footerLayout.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(headerPanel, 0, 0);
            rootLayout.Controls.Add(progressLayout, 0, 1);
            rootLayout.Controls.Add(formLayout, 0, 2);
            rootLayout.Controls.Add(footerLayout, 0, 3);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 88F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 68F));
            rootLayout.Size = new Size(784, 611);
            rootLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(lblFormTitle);
            headerPanel.Controls.Add(lblFormSubtitle);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Margin = new Padding(0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(784, 84);
            headerPanel.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            lblFormTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblFormTitle.Location = new Point(28, 16);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new Size(728, 30);
            lblFormTitle.TabIndex = 0;
            lblFormTitle.Text = "Report an Issue";
            // 
            // lblFormSubtitle
            // 
            lblFormSubtitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblFormSubtitle.Location = new Point(28, 50);
            lblFormSubtitle.Name = "lblFormSubtitle";
            lblFormSubtitle.Size = new Size(728, 22);
            lblFormSubtitle.TabIndex = 1;
            lblFormSubtitle.Text = "Tell us what is wrong and where, and we will log it for the responsible department.";
            // 
            // progressLayout
            // 
            progressLayout.ColumnCount = 1;
            progressLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            progressLayout.Controls.Add(lblEngagement, 0, 0);
            progressLayout.Controls.Add(prgReport, 0, 1);
            progressLayout.Dock = DockStyle.Fill;
            progressLayout.Location = new Point(28, 92);
            progressLayout.Margin = new Padding(28, 8, 28, 8);
            progressLayout.Name = "progressLayout";
            progressLayout.Padding = new Padding(14, 10, 14, 12);
            progressLayout.RowCount = 2;
            progressLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            progressLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            progressLayout.Size = new Size(728, 72);
            progressLayout.TabIndex = 1;
            // 
            // lblEngagement
            // 
            lblEngagement.Dock = DockStyle.Fill;
            lblEngagement.Location = new Point(17, 10);
            lblEngagement.Name = "lblEngagement";
            lblEngagement.Size = new Size(694, 26);
            lblEngagement.TabIndex = 0;
            lblEngagement.Text = "Let's get started. Begin by entering where the issue is.";
            lblEngagement.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // prgReport
            // 
            prgReport.Dock = DockStyle.Fill;
            prgReport.Location = new Point(17, 39);
            prgReport.Name = "prgReport";
            prgReport.Size = new Size(694, 18);
            prgReport.Step = 25;
            prgReport.Style = ProgressBarStyle.Continuous;
            prgReport.TabIndex = 1;
            // 
            // formLayout
            // 
            formLayout.ColumnCount = 2;
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            formLayout.Controls.Add(lblLocation, 0, 0);
            formLayout.Controls.Add(txtLocation, 1, 0);
            formLayout.Controls.Add(lblCategory, 0, 1);
            formLayout.Controls.Add(cboCategory, 1, 1);
            formLayout.Controls.Add(lblDescription, 0, 2);
            formLayout.Controls.Add(rtbDescription, 1, 2);
            formLayout.Controls.Add(lblAttachment, 0, 3);
            formLayout.Controls.Add(attachmentLayout, 1, 3);
            formLayout.Dock = DockStyle.Fill;
            formLayout.Location = new Point(28, 172);
            formLayout.Margin = new Padding(28, 0, 28, 0);
            formLayout.Name = "formLayout";
            formLayout.RowCount = 4;
            formLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            formLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            formLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            formLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            formLayout.Size = new Size(728, 371);
            formLayout.TabIndex = 2;
            // 
            // lblLocation
            // 
            lblLocation.Dock = DockStyle.Fill;
            lblLocation.Location = new Point(3, 0);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(154, 44);
            lblLocation.TabIndex = 0;
            lblLocation.Text = "Location *";
            lblLocation.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtLocation
            // 
            txtLocation.Dock = DockStyle.Fill;
            txtLocation.Location = new Point(163, 9);
            txtLocation.Margin = new Padding(3, 9, 3, 9);
            txtLocation.MaxLength = 150;
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(562, 23);
            txtLocation.TabIndex = 1;
            // 
            // lblCategory
            // 
            lblCategory.Dock = DockStyle.Fill;
            lblCategory.Location = new Point(3, 44);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(154, 44);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "Category *";
            lblCategory.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboCategory
            // 
            cboCategory.Dock = DockStyle.Fill;
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(163, 53);
            cboCategory.Margin = new Padding(3, 9, 3, 9);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(562, 23);
            cboCategory.TabIndex = 3;
            // 
            // lblDescription
            // 
            lblDescription.Dock = DockStyle.Fill;
            lblDescription.Location = new Point(3, 88);
            lblDescription.Name = "lblDescription";
            lblDescription.Padding = new Padding(0, 12, 0, 0);
            lblDescription.Size = new Size(154, 231);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Description *";
            // 
            // rtbDescription
            // 
            rtbDescription.Dock = DockStyle.Fill;
            rtbDescription.Location = new Point(163, 97);
            rtbDescription.Margin = new Padding(3, 9, 3, 9);
            rtbDescription.Name = "rtbDescription";
            rtbDescription.Size = new Size(562, 213);
            rtbDescription.TabIndex = 5;
            rtbDescription.Text = "";
            // 
            // lblAttachment
            // 
            lblAttachment.Dock = DockStyle.Fill;
            lblAttachment.Location = new Point(3, 319);
            lblAttachment.Name = "lblAttachment";
            lblAttachment.Size = new Size(154, 52);
            lblAttachment.TabIndex = 6;
            lblAttachment.Text = "Attachment";
            lblAttachment.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // attachmentLayout
            // 
            attachmentLayout.ColumnCount = 2;
            attachmentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
            attachmentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            attachmentLayout.Controls.Add(btnAttach, 0, 0);
            attachmentLayout.Controls.Add(lblAttachmentName, 1, 0);
            attachmentLayout.Dock = DockStyle.Fill;
            attachmentLayout.Location = new Point(160, 319);
            attachmentLayout.Margin = new Padding(0);
            attachmentLayout.Name = "attachmentLayout";
            attachmentLayout.RowCount = 1;
            attachmentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            attachmentLayout.Size = new Size(568, 52);
            attachmentLayout.TabIndex = 7;
            // 
            // btnAttach
            // 
            btnAttach.Dock = DockStyle.Fill;
            btnAttach.Location = new Point(3, 9);
            btnAttach.Margin = new Padding(3, 9, 3, 9);
            btnAttach.Name = "btnAttach";
            btnAttach.Size = new Size(174, 34);
            btnAttach.TabIndex = 0;
            btnAttach.Text = "Attach Photo or Document";
            btnAttach.UseVisualStyleBackColor = true;
            // 
            // lblAttachmentName
            // 
            lblAttachmentName.Dock = DockStyle.Fill;
            lblAttachmentName.Location = new Point(183, 0);
            lblAttachmentName.Name = "lblAttachmentName";
            lblAttachmentName.Size = new Size(382, 52);
            lblAttachmentName.TabIndex = 1;
            lblAttachmentName.Text = "No file attached (optional).";
            lblAttachmentName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // footerLayout
            // 
            footerLayout.ColumnCount = 4;
            footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            footerLayout.Controls.Add(btnBack, 0, 0);
            footerLayout.Controls.Add(btnClear, 2, 0);
            footerLayout.Controls.Add(btnSubmit, 3, 0);
            footerLayout.Dock = DockStyle.Fill;
            footerLayout.Location = new Point(28, 543);
            footerLayout.Margin = new Padding(28, 0, 28, 0);
            footerLayout.Name = "footerLayout";
            footerLayout.RowCount = 1;
            footerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            footerLayout.Size = new Size(728, 68);
            footerLayout.TabIndex = 3;
            // 
            // btnBack
            // 
            btnBack.Dock = DockStyle.Fill;
            btnBack.Location = new Point(3, 14);
            btnBack.Margin = new Padding(3, 14, 3, 18);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(164, 36);
            btnBack.TabIndex = 0;
            btnBack.Text = "Back to Main Menu";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Dock = DockStyle.Fill;
            btnClear.Location = new Point(441, 14);
            btnClear.Margin = new Padding(3, 14, 3, 18);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(114, 36);
            btnClear.TabIndex = 1;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnSubmit
            // 
            btnSubmit.Dock = DockStyle.Fill;
            btnSubmit.Location = new Point(561, 14);
            btnSubmit.Margin = new Padding(3, 14, 3, 18);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(164, 36);
            btnSubmit.TabIndex = 2;
            btnSubmit.Text = "Submit Report";
            btnSubmit.UseVisualStyleBackColor = true;
            // 
            // ReportIssuesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 611);
            Controls.Add(rootLayout);
            MinimumSize = new Size(700, 600);
            Name = "ReportIssuesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Municipal Services Application - Report an Issue";
            rootLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            progressLayout.ResumeLayout(false);
            formLayout.ResumeLayout(false);
            formLayout.PerformLayout();
            attachmentLayout.ResumeLayout(false);
            footerLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblFormSubtitle;
        private System.Windows.Forms.TableLayoutPanel progressLayout;
        private System.Windows.Forms.Label lblEngagement;
        private System.Windows.Forms.ProgressBar prgReport;
        private System.Windows.Forms.TableLayoutPanel formLayout;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.Label lblAttachment;
        private System.Windows.Forms.TableLayoutPanel attachmentLayout;
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.Label lblAttachmentName;
        private System.Windows.Forms.TableLayoutPanel footerLayout;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSubmit;
    }
}