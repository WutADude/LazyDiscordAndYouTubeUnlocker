using LazyDisYTUnlocker.Properties;

namespace LazyDisYTUnlocker.Forms
{
    public partial class DomainsAppenderForm : Form
    {
        public DomainsAppenderForm()
        {
            InitializeComponent();
        }

        private void SaveUserServicesButton_Click(object sender, EventArgs e)
        {
            FilesAndDirectories.RewriteUserServicesHostsFile(UserServicesLinesRB.Lines.Distinct());
            Close();
            MessageBox.Show(StringsLocalization.USSavingMessageText, StringsLocalization.USSavingMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DomainsAppenderForm_Shown(object sender, EventArgs e)
        {
            UserServicesLinesRB.Lines = FilesAndDirectories.GetUserServicesDomainsLines;
        }

        private void UserServicesLinesRB_TextChanged(object sender, EventArgs e)
        {
            if (UserServicesLinesRB.Lines.Any(l => l.Contains("https://") || l.Contains("http://")))
            {
                int oldPosition = UserServicesLinesRB.SelectionStart;
                UserServicesLinesRB.Text = UserServicesLinesRB.Text.Replace("https://", "").Replace("http://", "");
                UserServicesLinesRB.SelectionStart = oldPosition;
            }
        }
    }
}
