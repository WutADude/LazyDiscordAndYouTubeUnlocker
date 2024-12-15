namespace LazyDisYTUnlocker.Forms
{
    partial class DomainsAppenderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DomainsAppenderForm));
            TipsGB = new GroupBox();
            TipLabel = new Label();
            UserServicesGB = new GroupBox();
            UserServicesLinesRB = new RichTextBox();
            SaveUserServicesButton = new Button();
            TipsGB.SuspendLayout();
            UserServicesGB.SuspendLayout();
            SuspendLayout();
            // 
            // TipsGB
            // 
            resources.ApplyResources(TipsGB, "TipsGB");
            TipsGB.Controls.Add(TipLabel);
            TipsGB.Name = "TipsGB";
            TipsGB.TabStop = false;
            // 
            // TipLabel
            // 
            resources.ApplyResources(TipLabel, "TipLabel");
            TipLabel.Name = "TipLabel";
            // 
            // UserServicesGB
            // 
            resources.ApplyResources(UserServicesGB, "UserServicesGB");
            UserServicesGB.Controls.Add(UserServicesLinesRB);
            UserServicesGB.Name = "UserServicesGB";
            UserServicesGB.TabStop = false;
            // 
            // UserServicesLinesRB
            // 
            resources.ApplyResources(UserServicesLinesRB, "UserServicesLinesRB");
            UserServicesLinesRB.BorderStyle = BorderStyle.None;
            UserServicesLinesRB.Name = "UserServicesLinesRB";
            UserServicesLinesRB.TextChanged += UserServicesLinesRB_TextChanged;
            // 
            // SaveUserServicesButton
            // 
            resources.ApplyResources(SaveUserServicesButton, "SaveUserServicesButton");
            SaveUserServicesButton.Cursor = Cursors.Hand;
            SaveUserServicesButton.Name = "SaveUserServicesButton";
            SaveUserServicesButton.UseVisualStyleBackColor = true;
            SaveUserServicesButton.Click += SaveUserServicesButton_Click;
            // 
            // DomainsAppenderForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            Controls.Add(SaveUserServicesButton);
            Controls.Add(UserServicesGB);
            Controls.Add(TipsGB);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DomainsAppenderForm";
            ShowIcon = false;
            Shown += DomainsAppenderForm_Shown;
            TipsGB.ResumeLayout(false);
            UserServicesGB.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox TipsGB;
        private Label TipLabel;
        private GroupBox UserServicesGB;
        private RichTextBox UserServicesLinesRB;
        private Button SaveUserServicesButton;
    }
}