namespace LazyDisYTUnloker
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
            YouTubeDomainsCountLabel = new Label();
            DiscordDomainsCountLabel = new Label();
            BundleStatusLabel = new Label();
            Status = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            WorkStatusLabel = new ToolStripStatusLabel();
            InfoGB = new GroupBox();
            RepositoryZapretLabel = new Label();
            MainDevLabel = new Label();
            label1 = new Label();
            LauncherDevLabel = new Label();
            WarningInfoLabel = new Label();
            HideInTrayCB = new CheckBox();
            StatusGB.SuspendLayout();
            Status.SuspendLayout();
            InfoGB.SuspendLayout();
            SuspendLayout();
            // 
            // MainButton
            // 
            MainButton.Cursor = Cursors.Hand;
            MainButton.Enabled = false;
            MainButton.FlatStyle = FlatStyle.Flat;
            MainButton.Location = new Point(12, 12);
            MainButton.Name = "MainButton";
            MainButton.Size = new Size(501, 35);
            MainButton.TabIndex = 0;
            MainButton.Text = "Запустить";
            MainButton.UseVisualStyleBackColor = true;
            MainButton.Click += MainButton_Click;
            // 
            // StatusGB
            // 
            StatusGB.Controls.Add(YouTubeDomainsCountLabel);
            StatusGB.Controls.Add(DiscordDomainsCountLabel);
            StatusGB.Controls.Add(BundleStatusLabel);
            StatusGB.Location = new Point(12, 48);
            StatusGB.Name = "StatusGB";
            StatusGB.Size = new Size(501, 74);
            StatusGB.TabIndex = 1;
            StatusGB.TabStop = false;
            StatusGB.Text = "Статусная информация:";
            // 
            // YouTubeDomainsCountLabel
            // 
            YouTubeDomainsCountLabel.AutoSize = true;
            YouTubeDomainsCountLabel.Location = new Point(6, 49);
            YouTubeDomainsCountLabel.Name = "YouTubeDomainsCountLabel";
            YouTubeDomainsCountLabel.Size = new Size(187, 15);
            YouTubeDomainsCountLabel.TabIndex = 2;
            YouTubeDomainsCountLabel.Text = "Число доменов YouTube: {count}";
            // 
            // DiscordDomainsCountLabel
            // 
            DiscordDomainsCountLabel.AutoSize = true;
            DiscordDomainsCountLabel.Location = new Point(6, 34);
            DiscordDomainsCountLabel.Name = "DiscordDomainsCountLabel";
            DiscordDomainsCountLabel.Size = new Size(181, 15);
            DiscordDomainsCountLabel.TabIndex = 1;
            DiscordDomainsCountLabel.Text = "Число доменов Discord: {count}";
            // 
            // BundleStatusLabel
            // 
            BundleStatusLabel.AutoSize = true;
            BundleStatusLabel.Location = new Point(6, 19);
            BundleStatusLabel.Name = "BundleStatusLabel";
            BundleStatusLabel.Size = new Size(128, 15);
            BundleStatusLabel.TabIndex = 0;
            BundleStatusLabel.Text = "Запрет-бандл: {status}";
            // 
            // Status
            // 
            Status.BackColor = Color.Transparent;
            Status.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, WorkStatusLabel });
            Status.Location = new Point(0, 242);
            Status.Name = "Status";
            Status.Size = new Size(525, 22);
            Status.SizingGrip = false;
            Status.TabIndex = 2;
            Status.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(90, 17);
            toolStripStatusLabel1.Text = "Статус работы:";
            // 
            // WorkStatusLabel
            // 
            WorkStatusLabel.Name = "WorkStatusLabel";
            WorkStatusLabel.Size = new Size(126, 17);
            WorkStatusLabel.Text = "сейчас не работает :P";
            // 
            // InfoGB
            // 
            InfoGB.Controls.Add(RepositoryZapretLabel);
            InfoGB.Controls.Add(MainDevLabel);
            InfoGB.Controls.Add(label1);
            InfoGB.Controls.Add(LauncherDevLabel);
            InfoGB.Controls.Add(WarningInfoLabel);
            InfoGB.Location = new Point(12, 122);
            InfoGB.Name = "InfoGB";
            InfoGB.Size = new Size(501, 100);
            InfoGB.TabIndex = 3;
            InfoGB.TabStop = false;
            InfoGB.Text = "Информация";
            // 
            // RepositoryZapretLabel
            // 
            RepositoryZapretLabel.Cursor = Cursors.Hand;
            RepositoryZapretLabel.ForeColor = Color.DarkSeaGreen;
            RepositoryZapretLabel.Location = new Point(271, 52);
            RepositoryZapretLabel.Name = "RepositoryZapretLabel";
            RepositoryZapretLabel.Size = new Size(218, 21);
            RepositoryZapretLabel.TabIndex = 5;
            RepositoryZapretLabel.Text = "Репозиторий \"Zapret\"";
            RepositoryZapretLabel.TextAlign = ContentAlignment.MiddleCenter;
            RepositoryZapretLabel.Click += RepZapretLabel_Click;
            // 
            // MainDevLabel
            // 
            MainDevLabel.Cursor = Cursors.Hand;
            MainDevLabel.ForeColor = Color.ForestGreen;
            MainDevLabel.Location = new Point(12, 52);
            MainDevLabel.Name = "MainDevLabel";
            MainDevLabel.Size = new Size(218, 21);
            MainDevLabel.TabIndex = 4;
            MainDevLabel.Text = "Гений, разработчик \"запрета\": bol-van";
            MainDevLabel.TextAlign = ContentAlignment.MiddleCenter;
            MainDevLabel.Click += MainDevLabel_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 5F);
            label1.Location = new Point(12, 32);
            label1.Name = "label1";
            label1.Size = new Size(477, 10);
            label1.TabIndex = 3;
            label1.Text = "Сделал почти ничего но возможно упростил немного жизнь :Р";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LauncherDevLabel
            // 
            LauncherDevLabel.Cursor = Cursors.Hand;
            LauncherDevLabel.Location = new Point(12, 15);
            LauncherDevLabel.Name = "LauncherDevLabel";
            LauncherDevLabel.Size = new Size(477, 21);
            LauncherDevLabel.TabIndex = 2;
            LauncherDevLabel.Text = "Разработчик заглушки/лаунчера: WutADude (aka wDude)";
            LauncherDevLabel.TextAlign = ContentAlignment.MiddleCenter;
            LauncherDevLabel.Click += LauncherDevLabel_Click;
            // 
            // WarningInfoLabel
            // 
            WarningInfoLabel.ForeColor = Color.Red;
            WarningInfoLabel.Location = new Point(6, 82);
            WarningInfoLabel.Name = "WarningInfoLabel";
            WarningInfoLabel.Size = new Size(489, 15);
            WarningInfoLabel.TabIndex = 1;
            WarningInfoLabel.Text = "Разработчик заглушки не гарантирует работоспособность чего-либо после запуска!";
            WarningInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // HideInTrayCB
            // 
            HideInTrayCB.AutoSize = true;
            HideInTrayCB.Location = new Point(151, 223);
            HideInTrayCB.Name = "HideInTrayCB";
            HideInTrayCB.Size = new Size(219, 19);
            HideInTrayCB.TabIndex = 3;
            HideInTrayCB.Text = "Cпрятать в трей при сворачивании";
            HideInTrayCB.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(525, 264);
            Controls.Add(HideInTrayCB);
            Controls.Add(InfoGB);
            Controls.Add(Status);
            Controls.Add(StatusGB);
            Controls.Add(MainButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DS & YT unlock launcher (огромная благодарность bol-van <3)";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Resize += MainForm_Resize;
            StatusGB.ResumeLayout(false);
            StatusGB.PerformLayout();
            Status.ResumeLayout(false);
            Status.PerformLayout();
            InfoGB.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button MainButton;
        private GroupBox StatusGB;
        private Label DiscordDomainsCountLabel;
        private Label BundleStatusLabel;
        private Label YouTubeDomainsCountLabel;
        private StatusStrip Status;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel WorkStatusLabel;
        private GroupBox InfoGB;
        private Label LauncherDevLabel;
        private Label WarningInfoLabel;
        private Label MainDevLabel;
        private Label label1;
        private Label RepositoryZapretLabel;
        private CheckBox HideInTrayCB;
    }
}
