namespace LazyDisYTUnloker.Forms
{
    partial class InfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            RepositoryZapretLabel = new Label();
            label1 = new Label();
            LauncherDevLabel = new Label();
            splitter1 = new Splitter();
            LauncherInfoGB = new GroupBox();
            ZapretInfoGB = new GroupBox();
            label2 = new Label();
            LauncherInfoGB.SuspendLayout();
            ZapretInfoGB.SuspendLayout();
            SuspendLayout();
            // 
            // RepositoryZapretLabel
            // 
            RepositoryZapretLabel.Cursor = Cursors.Hand;
            RepositoryZapretLabel.ForeColor = SystemColors.WindowText;
            RepositoryZapretLabel.Location = new Point(8, 20);
            RepositoryZapretLabel.Name = "RepositoryZapretLabel";
            RepositoryZapretLabel.Size = new Size(482, 21);
            RepositoryZapretLabel.TabIndex = 9;
            RepositoryZapretLabel.Text = "Репозиторий \"Zapret\" и разработчик bol-van";
            RepositoryZapretLabel.TextAlign = ContentAlignment.MiddleCenter;
            RepositoryZapretLabel.Click += RepZapretLabel_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 5F);
            label1.Location = new Point(12, 37);
            label1.Name = "label1";
            label1.Size = new Size(470, 10);
            label1.TabIndex = 7;
            label1.Text = "Сделал почти ничего но возможно упростил немного жизнь :Р";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LauncherDevLabel
            // 
            LauncherDevLabel.Cursor = Cursors.Hand;
            LauncherDevLabel.Location = new Point(6, 19);
            LauncherDevLabel.Name = "LauncherDevLabel";
            LauncherDevLabel.Size = new Size(482, 21);
            LauncherDevLabel.TabIndex = 6;
            LauncherDevLabel.Text = "Разработчик лаунчера: WutADude (aka wDude)";
            LauncherDevLabel.TextAlign = ContentAlignment.MiddleCenter;
            LauncherDevLabel.Click += LauncherDevLabel_Click;
            // 
            // splitter1
            // 
            splitter1.Location = new Point(0, 0);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 139);
            splitter1.TabIndex = 10;
            splitter1.TabStop = false;
            // 
            // LauncherInfoGB
            // 
            LauncherInfoGB.Controls.Add(LauncherDevLabel);
            LauncherInfoGB.Controls.Add(label1);
            LauncherInfoGB.Location = new Point(12, 2);
            LauncherInfoGB.Name = "LauncherInfoGB";
            LauncherInfoGB.Size = new Size(494, 64);
            LauncherInfoGB.TabIndex = 11;
            LauncherInfoGB.TabStop = false;
            LauncherInfoGB.Text = "Лаунчер";
            // 
            // ZapretInfoGB
            // 
            ZapretInfoGB.Controls.Add(label2);
            ZapretInfoGB.Controls.Add(RepositoryZapretLabel);
            ZapretInfoGB.Location = new Point(10, 66);
            ZapretInfoGB.Name = "ZapretInfoGB";
            ZapretInfoGB.Size = new Size(496, 59);
            ZapretInfoGB.TabIndex = 12;
            ZapretInfoGB.TabStop = false;
            ZapretInfoGB.Text = "Zapret";
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 5F);
            label2.Location = new Point(13, 38);
            label2.Name = "label2";
            label2.Size = new Size(470, 10);
            label2.TabIndex = 10;
            label2.Text = "То и тот, на ком всё закручено";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // InfoForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = SystemColors.Window;
            ClientSize = new Size(515, 139);
            Controls.Add(ZapretInfoGB);
            Controls.Add(LauncherInfoGB);
            Controls.Add(splitter1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InfoForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "О программе";
            LauncherInfoGB.ResumeLayout(false);
            ZapretInfoGB.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label RepositoryZapretLabel;
        private Label label1;
        private Label LauncherDevLabel;
        private Splitter splitter1;
        private GroupBox LauncherInfoGB;
        private GroupBox ZapretInfoGB;
        private Label label2;
    }
}