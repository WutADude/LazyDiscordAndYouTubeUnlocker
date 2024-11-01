namespace LazyDisYTUnlocker.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
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
            resources.ApplyResources(RepositoryZapretLabel, "RepositoryZapretLabel");
            RepositoryZapretLabel.Cursor = Cursors.Hand;
            RepositoryZapretLabel.ForeColor = SystemColors.WindowText;
            RepositoryZapretLabel.Name = "RepositoryZapretLabel";
            RepositoryZapretLabel.Click += RepZapretLabel_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // LauncherDevLabel
            // 
            resources.ApplyResources(LauncherDevLabel, "LauncherDevLabel");
            LauncherDevLabel.Cursor = Cursors.Hand;
            LauncherDevLabel.Name = "LauncherDevLabel";
            LauncherDevLabel.Click += LauncherDevLabel_Click;
            // 
            // splitter1
            // 
            resources.ApplyResources(splitter1, "splitter1");
            splitter1.Name = "splitter1";
            splitter1.TabStop = false;
            // 
            // LauncherInfoGB
            // 
            resources.ApplyResources(LauncherInfoGB, "LauncherInfoGB");
            LauncherInfoGB.Controls.Add(LauncherDevLabel);
            LauncherInfoGB.Controls.Add(label1);
            LauncherInfoGB.Name = "LauncherInfoGB";
            LauncherInfoGB.TabStop = false;
            // 
            // ZapretInfoGB
            // 
            resources.ApplyResources(ZapretInfoGB, "ZapretInfoGB");
            ZapretInfoGB.Controls.Add(label2);
            ZapretInfoGB.Controls.Add(RepositoryZapretLabel);
            ZapretInfoGB.Name = "ZapretInfoGB";
            ZapretInfoGB.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // InfoForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.Window;
            Controls.Add(ZapretInfoGB);
            Controls.Add(LauncherInfoGB);
            Controls.Add(splitter1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InfoForm";
            ShowIcon = false;
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