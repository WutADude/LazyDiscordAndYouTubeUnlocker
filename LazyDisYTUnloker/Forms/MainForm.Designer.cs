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
            ReinstallZapretLinkLabel = new LinkLabel();
            Status = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            SoftStatus = new ToolStripStatusLabel();
            InfoGB = new GroupBox();
            SoftwareAboutLabel = new Label();
            SoftwareVersionLabel = new Label();
            WarningInfoLabel = new Label();
            HideInTrayCB = new CheckBox();
            AdditionalOptionsGB = new GroupBox();
            WindivertServiceCB = new CheckBox();
            StrategiesUpdateDateLabel = new Label();
            UpdateStrategiesButton = new Button();
            label2 = new Label();
            StrategiesInfoGB = new GroupBox();
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
            StatusGB.Location = new Point(12, 63);
            StatusGB.Name = "StatusGB";
            StatusGB.Size = new Size(501, 74);
            StatusGB.TabIndex = 1;
            StatusGB.TabStop = false;
            StatusGB.Text = "Статусная информация:";
            // 
            // YouTubeDomainsCountLabel
            // 
            YouTubeDomainsCountLabel.AutoSize = true;
            YouTubeDomainsCountLabel.Location = new Point(6, 50);
            YouTubeDomainsCountLabel.Name = "YouTubeDomainsCountLabel";
            YouTubeDomainsCountLabel.Size = new Size(157, 15);
            YouTubeDomainsCountLabel.TabIndex = 2;
            YouTubeDomainsCountLabel.Text = "Число доменов YouTube: ...";
            // 
            // DiscordDomainsCountLabel
            // 
            DiscordDomainsCountLabel.AutoSize = true;
            DiscordDomainsCountLabel.Location = new Point(6, 34);
            DiscordDomainsCountLabel.Name = "DiscordDomainsCountLabel";
            DiscordDomainsCountLabel.Size = new Size(151, 15);
            DiscordDomainsCountLabel.TabIndex = 1;
            DiscordDomainsCountLabel.Text = "Число доменов Discord: ...";
            // 
            // BundleStatusLabel
            // 
            BundleStatusLabel.AutoSize = true;
            BundleStatusLabel.Location = new Point(6, 18);
            BundleStatusLabel.Name = "BundleStatusLabel";
            BundleStatusLabel.Size = new Size(118, 15);
            BundleStatusLabel.TabIndex = 0;
            BundleStatusLabel.Text = "Состояние Zapret: ...";
            // 
            // ReinstallZapretLinkLabel
            // 
            ReinstallZapretLinkLabel.ActiveLinkColor = Color.RosyBrown;
            ReinstallZapretLinkLabel.AutoSize = true;
            ReinstallZapretLinkLabel.Cursor = Cursors.Hand;
            ReinstallZapretLinkLabel.Font = new Font("Segoe UI", 6.5F);
            ReinstallZapretLinkLabel.LinkBehavior = LinkBehavior.NeverUnderline;
            ReinstallZapretLinkLabel.LinkColor = SystemColors.ControlText;
            ReinstallZapretLinkLabel.Location = new Point(271, 17);
            ReinstallZapretLinkLabel.Name = "ReinstallZapretLinkLabel";
            ReinstallZapretLinkLabel.Size = new Size(145, 12);
            ReinstallZapretLinkLabel.TabIndex = 3;
            ReinstallZapretLinkLabel.TabStop = true;
            ReinstallZapretLinkLabel.Text = "Переустановить/Обновить Zapret";
            ReinstallZapretLinkLabel.TextAlign = ContentAlignment.MiddleCenter;
            ReinstallZapretLinkLabel.VisitedLinkColor = Color.Transparent;
            ReinstallZapretLinkLabel.LinkClicked += ReinstallZapretLinkLabel_LinkClicked;
            // 
            // Status
            // 
            Status.AutoSize = false;
            Status.BackColor = Color.Transparent;
            Status.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, SoftStatus });
            Status.Location = new Point(0, 387);
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
            // SoftStatus
            // 
            SoftStatus.Name = "SoftStatus";
            SoftStatus.Size = new Size(16, 17);
            SoftStatus.Text = "...";
            // 
            // InfoGB
            // 
            InfoGB.Controls.Add(ReinstallZapretLinkLabel);
            InfoGB.Controls.Add(SoftwareAboutLabel);
            InfoGB.Controls.Add(SoftwareVersionLabel);
            InfoGB.Location = new Point(12, 349);
            InfoGB.Name = "InfoGB";
            InfoGB.Size = new Size(501, 39);
            InfoGB.TabIndex = 3;
            InfoGB.TabStop = false;
            // 
            // SoftwareAboutLabel
            // 
            SoftwareAboutLabel.Cursor = Cursors.Hand;
            SoftwareAboutLabel.Font = new Font("Segoe UI", 6.5F);
            SoftwareAboutLabel.Location = new Point(422, 17);
            SoftwareAboutLabel.Name = "SoftwareAboutLabel";
            SoftwareAboutLabel.Size = new Size(67, 13);
            SoftwareAboutLabel.TabIndex = 9;
            SoftwareAboutLabel.Text = "О программе\r\n";
            SoftwareAboutLabel.TextAlign = ContentAlignment.MiddleRight;
            SoftwareAboutLabel.Click += SoftwareAboutLabel_Click;
            // 
            // SoftwareVersionLabel
            // 
            SoftwareVersionLabel.AutoSize = true;
            SoftwareVersionLabel.Cursor = Cursors.Hand;
            SoftwareVersionLabel.Font = new Font("Segoe UI", 6.5F);
            SoftwareVersionLabel.Location = new Point(12, 17);
            SoftwareVersionLabel.Name = "SoftwareVersionLabel";
            SoftwareVersionLabel.Size = new Size(146, 12);
            SoftwareVersionLabel.TabIndex = 8;
            SoftwareVersionLabel.Text = "Текущая версия: ... (актуальная: ...)";
            SoftwareVersionLabel.TextAlign = ContentAlignment.MiddleLeft;
            SoftwareVersionLabel.Click += SoftwareVersionLabel_Click;
            // 
            // WarningInfoLabel
            // 
            WarningInfoLabel.ForeColor = Color.Red;
            WarningInfoLabel.Location = new Point(12, 48);
            WarningInfoLabel.Name = "WarningInfoLabel";
            WarningInfoLabel.Size = new Size(501, 15);
            WarningInfoLabel.TabIndex = 1;
            WarningInfoLabel.Text = "Разработчик заглушки не гарантирует работоспособность чего-либо после запуска!";
            WarningInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // HideInTrayCB
            // 
            HideInTrayCB.AutoSize = true;
            HideInTrayCB.Location = new Point(12, 17);
            HideInTrayCB.Name = "HideInTrayCB";
            HideInTrayCB.Size = new Size(249, 19);
            HideInTrayCB.TabIndex = 3;
            HideInTrayCB.Text = "Cпрятать окно в трей при сворачивании";
            HideInTrayCB.UseVisualStyleBackColor = true;
            // 
            // AdditionalOptionsGB
            // 
            AdditionalOptionsGB.Controls.Add(WindivertServiceCB);
            AdditionalOptionsGB.Controls.Add(HideInTrayCB);
            AdditionalOptionsGB.Location = new Point(12, 289);
            AdditionalOptionsGB.Name = "AdditionalOptionsGB";
            AdditionalOptionsGB.Size = new Size(501, 60);
            AdditionalOptionsGB.TabIndex = 4;
            AdditionalOptionsGB.TabStop = false;
            AdditionalOptionsGB.Text = "Доп. параметры:";
            // 
            // WindivertServiceCB
            // 
            WindivertServiceCB.AutoSize = true;
            WindivertServiceCB.Location = new Point(12, 34);
            WindivertServiceCB.Name = "WindivertServiceCB";
            WindivertServiceCB.Size = new Size(283, 19);
            WindivertServiceCB.TabIndex = 4;
            WindivertServiceCB.Text = "Отключаем WinDivert сервис после остановки";
            WindivertServiceCB.UseVisualStyleBackColor = true;
            // 
            // StrategiesUpdateDateLabel
            // 
            StrategiesUpdateDateLabel.Font = new Font("Segoe UI", 6F);
            StrategiesUpdateDateLabel.Location = new Point(6, 132);
            StrategiesUpdateDateLabel.Name = "StrategiesUpdateDateLabel";
            StrategiesUpdateDateLabel.Size = new Size(489, 13);
            StrategiesUpdateDateLabel.TabIndex = 7;
            StrategiesUpdateDateLabel.Text = "Дата последнего обновления стратегий: ...";
            StrategiesUpdateDateLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UpdateStrategiesButton
            // 
            UpdateStrategiesButton.Cursor = Cursors.Hand;
            UpdateStrategiesButton.Enabled = false;
            UpdateStrategiesButton.FlatStyle = FlatStyle.Flat;
            UpdateStrategiesButton.Location = new Point(6, 105);
            UpdateStrategiesButton.Name = "UpdateStrategiesButton";
            UpdateStrategiesButton.Size = new Size(489, 24);
            UpdateStrategiesButton.TabIndex = 6;
            UpdateStrategiesButton.Text = "Обновить стратегии обхода замедлений/блокировок";
            UpdateStrategiesButton.UseVisualStyleBackColor = true;
            UpdateStrategiesButton.Click += UpdateStrategiesButton_Click;
            // 
            // label2
            // 
            label2.ForeColor = SystemColors.WindowText;
            label2.Location = new Point(6, 87);
            label2.Name = "label2";
            label2.Size = new Size(489, 15);
            label2.TabIndex = 5;
            label2.Text = "Что-то не работает? Попробуйте нажать на кнопку ниже чтобы обновить стратегии";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // StrategiesInfoGB
            // 
            StrategiesInfoGB.Controls.Add(DSStrategiesLabel);
            StrategiesInfoGB.Controls.Add(ChangeDSStrategyButton);
            StrategiesInfoGB.Controls.Add(ChangeYTStrategyButton);
            StrategiesInfoGB.Controls.Add(YTStrategiesCountLabel);
            StrategiesInfoGB.Controls.Add(StrategiesUpdateDateLabel);
            StrategiesInfoGB.Controls.Add(UpdateStrategiesButton);
            StrategiesInfoGB.Controls.Add(label2);
            StrategiesInfoGB.Location = new Point(12, 137);
            StrategiesInfoGB.Name = "StrategiesInfoGB";
            StrategiesInfoGB.Size = new Size(501, 152);
            StrategiesInfoGB.TabIndex = 3;
            StrategiesInfoGB.TabStop = false;
            StrategiesInfoGB.Text = "Стратегии обхода замедлений/блокировок";
            // 
            // DSStrategiesLabel
            // 
            DSStrategiesLabel.Location = new Point(6, 36);
            DSStrategiesLabel.Name = "DSStrategiesLabel";
            DSStrategiesLabel.Size = new Size(489, 23);
            DSStrategiesLabel.TabIndex = 11;
            DSStrategiesLabel.Text = "Число доступных стратегий Discord: ... | Выбранная стратегия: ...";
            DSStrategiesLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ChangeDSStrategyButton
            // 
            ChangeDSStrategyButton.Cursor = Cursors.Hand;
            ChangeDSStrategyButton.Enabled = false;
            ChangeDSStrategyButton.FlatStyle = FlatStyle.Flat;
            ChangeDSStrategyButton.Location = new Point(267, 59);
            ChangeDSStrategyButton.Name = "ChangeDSStrategyButton";
            ChangeDSStrategyButton.Size = new Size(225, 24);
            ChangeDSStrategyButton.TabIndex = 10;
            ChangeDSStrategyButton.Text = "Изменить стратегию для Discord";
            ChangeDSStrategyButton.UseVisualStyleBackColor = true;
            ChangeDSStrategyButton.Click += ChangeDSStrategyButton_Click;
            // 
            // ChangeYTStrategyButton
            // 
            ChangeYTStrategyButton.Cursor = Cursors.Hand;
            ChangeYTStrategyButton.Enabled = false;
            ChangeYTStrategyButton.FlatStyle = FlatStyle.Flat;
            ChangeYTStrategyButton.Location = new Point(9, 59);
            ChangeYTStrategyButton.Name = "ChangeYTStrategyButton";
            ChangeYTStrategyButton.Size = new Size(225, 24);
            ChangeYTStrategyButton.TabIndex = 9;
            ChangeYTStrategyButton.Text = "Изменить стратегию для YouTube";
            ChangeYTStrategyButton.UseVisualStyleBackColor = true;
            ChangeYTStrategyButton.Click += ChangeYTStrategyButton_Click;
            // 
            // YTStrategiesCountLabel
            // 
            YTStrategiesCountLabel.Location = new Point(6, 17);
            YTStrategiesCountLabel.Name = "YTStrategiesCountLabel";
            YTStrategiesCountLabel.Size = new Size(489, 23);
            YTStrategiesCountLabel.TabIndex = 8;
            YTStrategiesCountLabel.Text = "Число доступных стратегий YouTube: ... | Выбранная стратегия: ...";
            YTStrategiesCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(525, 409);
            Controls.Add(StrategiesInfoGB);
            Controls.Add(AdditionalOptionsGB);
            Controls.Add(InfoGB);
            Controls.Add(Status);
            Controls.Add(StatusGB);
            Controls.Add(WarningInfoLabel);
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
            InfoGB.PerformLayout();
            AdditionalOptionsGB.ResumeLayout(false);
            AdditionalOptionsGB.PerformLayout();
            StrategiesInfoGB.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button MainButton;
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
        private Button UpdateStrategiesButton;
        private Label label2;
        private Label StrategiesUpdateDateLabel;
        private Label SoftwareAboutLabel;
        private Label SoftwareVersionLabel;
        private GroupBox StrategiesInfoGB;
        private Label YTStrategiesCountLabel;
        private Label DSStrategiesLabel;
        public Button ChangeDSStrategyButton;
        public Button ChangeYTStrategyButton;
        private Button button1;
        private LinkLabel ReinstallZapretLinkLabel;
    }
}
