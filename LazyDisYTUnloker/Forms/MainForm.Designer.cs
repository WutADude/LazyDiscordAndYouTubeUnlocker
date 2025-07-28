namespace LazyDisYTUnlocker
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MainButton = new Button();
            StatusGB = new GroupBox();
            UserServicesDomainsCountLabel = new Label();
            YouTubeDomainsCountLabel = new Label();
            DiscordDomainsCountLabel = new Label();
            BundleStatusLabel = new Label();
            ReinstallZapretLinkLabel = new LinkLabel();
            Status = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            SoftStatus = new ToolStripStatusLabel();
            InfoGB = new GroupBox();
            LocalizationLabel = new Label();
            SoftwareAboutLabel = new Label();
            SoftwareVersionLabel = new Label();
            WarningInfoLabel = new Label();
            HideInTrayCB = new CheckBox();
            AdditionalOptionsGB = new GroupBox();
            AutoRunCB = new CheckBox();
            WindivertServiceCB = new CheckBox();
            StrategiesUpdateDateLabel = new Label();
            UpdateHostsAndStrategiesButton = new Button();
            label2 = new Label();
            StrategiesInfoGB = new GroupBox();
            ChangeUserServicesStrategiesButton = new Button();
            UserServicesStrategiesLabel = new Label();
            DSStrategiesLabel = new Label();
            ChangeDSStrategyButton = new Button();
            ChangeYTStrategyButton = new Button();
            YTStrategiesCountLabel = new Label();
            StatusGB.SuspendLayout();
            Status.SuspendLayout();
            InfoGB.SuspendLayout();
            AdditionalOptionsGB.SuspendLayout();
            StrategiesInfoGB.SuspendLayout();
            SuspendLayout();
            // 
            // MainButton
            // 
            resources.ApplyResources(MainButton, "MainButton");
            MainButton.Cursor = Cursors.Hand;
            MainButton.Name = "MainButton";
            MainButton.UseVisualStyleBackColor = true;
            MainButton.Click += MainButton_Click;
            // 
            // StatusGB
            // 
            resources.ApplyResources(StatusGB, "StatusGB");
            StatusGB.Controls.Add(UserServicesDomainsCountLabel);
            StatusGB.Controls.Add(YouTubeDomainsCountLabel);
            StatusGB.Controls.Add(DiscordDomainsCountLabel);
            StatusGB.Controls.Add(BundleStatusLabel);
            StatusGB.Name = "StatusGB";
            StatusGB.TabStop = false;
            // 
            // UserServicesDomainsCountLabel
            // 
            resources.ApplyResources(UserServicesDomainsCountLabel, "UserServicesDomainsCountLabel");
            UserServicesDomainsCountLabel.Cursor = Cursors.Hand;
            UserServicesDomainsCountLabel.Name = "UserServicesDomainsCountLabel";
            UserServicesDomainsCountLabel.Click += UserServicesDomainsCountLabel_Click;
            UserServicesDomainsCountLabel.MouseEnter += UserServicesDomainsCountLabel_MouseEnter;
            UserServicesDomainsCountLabel.MouseLeave += UserServicesDomainsCountLabel_MouseLeave;
            // 
            // YouTubeDomainsCountLabel
            // 
            resources.ApplyResources(YouTubeDomainsCountLabel, "YouTubeDomainsCountLabel");
            YouTubeDomainsCountLabel.Name = "YouTubeDomainsCountLabel";
            // 
            // DiscordDomainsCountLabel
            // 
            resources.ApplyResources(DiscordDomainsCountLabel, "DiscordDomainsCountLabel");
            DiscordDomainsCountLabel.Name = "DiscordDomainsCountLabel";
            // 
            // BundleStatusLabel
            // 
            resources.ApplyResources(BundleStatusLabel, "BundleStatusLabel");
            BundleStatusLabel.Name = "BundleStatusLabel";
            // 
            // ReinstallZapretLinkLabel
            // 
            resources.ApplyResources(ReinstallZapretLinkLabel, "ReinstallZapretLinkLabel");
            ReinstallZapretLinkLabel.ActiveLinkColor = Color.RosyBrown;
            ReinstallZapretLinkLabel.Cursor = Cursors.Hand;
            ReinstallZapretLinkLabel.LinkBehavior = LinkBehavior.NeverUnderline;
            ReinstallZapretLinkLabel.LinkColor = SystemColors.ControlText;
            ReinstallZapretLinkLabel.Name = "ReinstallZapretLinkLabel";
            ReinstallZapretLinkLabel.TabStop = true;
            ReinstallZapretLinkLabel.VisitedLinkColor = Color.Transparent;
            ReinstallZapretLinkLabel.LinkClicked += ReinstallZapretLinkLabel_LinkClicked;
            // 
            // Status
            // 
            resources.ApplyResources(Status, "Status");
            Status.BackColor = Color.Transparent;
            Status.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, SoftStatus });
            Status.Name = "Status";
            Status.SizingGrip = false;
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // SoftStatus
            // 
            resources.ApplyResources(SoftStatus, "SoftStatus");
            SoftStatus.Name = "SoftStatus";
            // 
            // InfoGB
            // 
            resources.ApplyResources(InfoGB, "InfoGB");
            InfoGB.Controls.Add(LocalizationLabel);
            InfoGB.Controls.Add(ReinstallZapretLinkLabel);
            InfoGB.Controls.Add(SoftwareAboutLabel);
            InfoGB.Controls.Add(SoftwareVersionLabel);
            InfoGB.Name = "InfoGB";
            InfoGB.TabStop = false;
            // 
            // LocalizationLabel
            // 
            resources.ApplyResources(LocalizationLabel, "LocalizationLabel");
            LocalizationLabel.Cursor = Cursors.Hand;
            LocalizationLabel.Name = "LocalizationLabel";
            LocalizationLabel.Click += LocalizationLabel_Click;
            // 
            // SoftwareAboutLabel
            // 
            resources.ApplyResources(SoftwareAboutLabel, "SoftwareAboutLabel");
            SoftwareAboutLabel.Cursor = Cursors.Hand;
            SoftwareAboutLabel.Name = "SoftwareAboutLabel";
            SoftwareAboutLabel.Click += SoftwareAboutLabel_Click;
            // 
            // SoftwareVersionLabel
            // 
            resources.ApplyResources(SoftwareVersionLabel, "SoftwareVersionLabel");
            SoftwareVersionLabel.Cursor = Cursors.Hand;
            SoftwareVersionLabel.Name = "SoftwareVersionLabel";
            SoftwareVersionLabel.Click += SoftwareVersionLabel_Click;
            // 
            // WarningInfoLabel
            // 
            resources.ApplyResources(WarningInfoLabel, "WarningInfoLabel");
            WarningInfoLabel.ForeColor = Color.Red;
            WarningInfoLabel.Name = "WarningInfoLabel";
            // 
            // HideInTrayCB
            // 
            resources.ApplyResources(HideInTrayCB, "HideInTrayCB");
            HideInTrayCB.Name = "HideInTrayCB";
            HideInTrayCB.UseVisualStyleBackColor = true;
            HideInTrayCB.Click += HideInTrayCB_Click;
            // 
            // AdditionalOptionsGB
            // 
            resources.ApplyResources(AdditionalOptionsGB, "AdditionalOptionsGB");
            AdditionalOptionsGB.Controls.Add(AutoRunCB);
            AdditionalOptionsGB.Controls.Add(WindivertServiceCB);
            AdditionalOptionsGB.Controls.Add(HideInTrayCB);
            AdditionalOptionsGB.Name = "AdditionalOptionsGB";
            AdditionalOptionsGB.TabStop = false;
            // 
            // AutoRunCB
            // 
            resources.ApplyResources(AutoRunCB, "AutoRunCB");
            AutoRunCB.Name = "AutoRunCB";
            AutoRunCB.UseVisualStyleBackColor = true;
            AutoRunCB.Click += AutoRunCB_Click;
            // 
            // WindivertServiceCB
            // 
            resources.ApplyResources(WindivertServiceCB, "WindivertServiceCB");
            WindivertServiceCB.Name = "WindivertServiceCB";
            WindivertServiceCB.UseVisualStyleBackColor = true;
            WindivertServiceCB.Click += WindivertServiceCB_Click;
            // 
            // StrategiesUpdateDateLabel
            // 
            resources.ApplyResources(StrategiesUpdateDateLabel, "StrategiesUpdateDateLabel");
            StrategiesUpdateDateLabel.Name = "StrategiesUpdateDateLabel";
            // 
            // UpdateHostsAndStrategiesButton
            // 
            resources.ApplyResources(UpdateHostsAndStrategiesButton, "UpdateHostsAndStrategiesButton");
            UpdateHostsAndStrategiesButton.Cursor = Cursors.Hand;
            UpdateHostsAndStrategiesButton.Name = "UpdateHostsAndStrategiesButton";
            UpdateHostsAndStrategiesButton.UseVisualStyleBackColor = true;
            UpdateHostsAndStrategiesButton.Click += UpdateStrategiesButton_Click;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.ForeColor = SystemColors.WindowText;
            label2.Name = "label2";
            // 
            // StrategiesInfoGB
            // 
            resources.ApplyResources(StrategiesInfoGB, "StrategiesInfoGB");
            StrategiesInfoGB.Controls.Add(ChangeUserServicesStrategiesButton);
            StrategiesInfoGB.Controls.Add(UserServicesStrategiesLabel);
            StrategiesInfoGB.Controls.Add(DSStrategiesLabel);
            StrategiesInfoGB.Controls.Add(ChangeDSStrategyButton);
            StrategiesInfoGB.Controls.Add(ChangeYTStrategyButton);
            StrategiesInfoGB.Controls.Add(YTStrategiesCountLabel);
            StrategiesInfoGB.Controls.Add(StrategiesUpdateDateLabel);
            StrategiesInfoGB.Controls.Add(UpdateHostsAndStrategiesButton);
            StrategiesInfoGB.Controls.Add(label2);
            StrategiesInfoGB.Name = "StrategiesInfoGB";
            StrategiesInfoGB.TabStop = false;
            // 
            // ChangeUserServicesStrategiesButton
            // 
            resources.ApplyResources(ChangeUserServicesStrategiesButton, "ChangeUserServicesStrategiesButton");
            ChangeUserServicesStrategiesButton.Cursor = Cursors.Hand;
            ChangeUserServicesStrategiesButton.Name = "ChangeUserServicesStrategiesButton";
            ChangeUserServicesStrategiesButton.UseVisualStyleBackColor = true;
            ChangeUserServicesStrategiesButton.Click += ChangeUserServicesStrategiesButton_Click;
            // 
            // UserServicesStrategiesLabel
            // 
            resources.ApplyResources(UserServicesStrategiesLabel, "UserServicesStrategiesLabel");
            UserServicesStrategiesLabel.Name = "UserServicesStrategiesLabel";
            // 
            // DSStrategiesLabel
            // 
            resources.ApplyResources(DSStrategiesLabel, "DSStrategiesLabel");
            DSStrategiesLabel.Name = "DSStrategiesLabel";
            // 
            // ChangeDSStrategyButton
            // 
            resources.ApplyResources(ChangeDSStrategyButton, "ChangeDSStrategyButton");
            ChangeDSStrategyButton.Cursor = Cursors.Hand;
            ChangeDSStrategyButton.Name = "ChangeDSStrategyButton";
            ChangeDSStrategyButton.UseVisualStyleBackColor = true;
            ChangeDSStrategyButton.Click += ChangeDSStrategyButton_Click;
            // 
            // ChangeYTStrategyButton
            // 
            resources.ApplyResources(ChangeYTStrategyButton, "ChangeYTStrategyButton");
            ChangeYTStrategyButton.Cursor = Cursors.Hand;
            ChangeYTStrategyButton.Name = "ChangeYTStrategyButton";
            ChangeYTStrategyButton.UseVisualStyleBackColor = true;
            ChangeYTStrategyButton.Click += ChangeYTStrategyButton_Click;
            // 
            // YTStrategiesCountLabel
            // 
            resources.ApplyResources(YTStrategiesCountLabel, "YTStrategiesCountLabel");
            YTStrategiesCountLabel.Name = "YTStrategiesCountLabel";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.Window;
            Controls.Add(StrategiesInfoGB);
            Controls.Add(AdditionalOptionsGB);
            Controls.Add(InfoGB);
            Controls.Add(Status);
            Controls.Add(StatusGB);
            Controls.Add(WarningInfoLabel);
            Controls.Add(MainButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            ShowIcon = false;
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Resize += MainForm_Resize;
            StatusGB.ResumeLayout(false);
            StatusGB.PerformLayout();
            Status.ResumeLayout(false);
            Status.PerformLayout();
            InfoGB.ResumeLayout(false);
            InfoGB.PerformLayout();
            AdditionalOptionsGB.ResumeLayout(false);
            AdditionalOptionsGB.PerformLayout();
            StrategiesInfoGB.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private GroupBox StatusGB;
        private Label DiscordDomainsCountLabel;
        private Label BundleStatusLabel;
        private Label YouTubeDomainsCountLabel;
        private StatusStrip Status;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel SoftStatus;
        private GroupBox InfoGB;
        private Label WarningInfoLabel;
        private CheckBox HideInTrayCB;
        private GroupBox AdditionalOptionsGB;
        public CheckBox WindivertServiceCB;
        private Label label2;
        private Label StrategiesUpdateDateLabel;
        private Label SoftwareAboutLabel;
        private Label SoftwareVersionLabel;
        private GroupBox StrategiesInfoGB;
        private Label YTStrategiesCountLabel;
        private Label DSStrategiesLabel;
        public Button ChangeDSStrategyButton;
        public Button ChangeYTStrategyButton;
        private LinkLabel ReinstallZapretLinkLabel;
        private Label LocalizationLabel;
        private Label UserServicesDomainsCountLabel;
        private Label UserServicesStrategiesLabel;
        public Button ChangeUserServicesStrategiesButton;
        public Button MainButton;
        public Button UpdateHostsAndStrategiesButton;
        private CheckBox AutoRunCB;
    }
}
