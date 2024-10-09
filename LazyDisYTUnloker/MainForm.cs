using System.Diagnostics;

namespace LazyDisYTUnloker
{
    public partial class MainForm : Form
    {
        private bool currentlyWorking = false;
        NotifyIcon? notifIcon { get; set; } = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckFilesAndSetup().ConfigureAwait(false);
        }

        private async Task CheckFilesAndSetup()
        {
            Task.Run(async() =>
            {
                FilesAndDirectories.Form = this;
                if (!FilesAndDirectories.IsZapretBundleDirectoriesLoaded())
                {
                    if (MessageBox.Show("Для работы нужно догрузить \"Zapret\" и подготовить кое какие файлы, делаем?", "Чего-то не хватает, но чего?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        FilesAndDirectories.SetupDirectory();
                        if (await FilesAndDirectories.DownloadUnpackAndSetupZapret() && FilesAndDirectories.IsZapretBundleDirectoriesLoaded())
                        {
                            ChangeZapretBundleStatus("готов к работе");
                            BeginInvoke(() => MainButton.Enabled = true);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Без этого ничего работать не будет, но это ваше право :)\n\n" +
                            "" +
                            "Если передумаете - просто перезапустите программу!", "Ну чтож... как хотите!");
                    }
                }
                else
                    BeginInvoke(() => MainButton.Enabled = true);
            });
        }


        internal void ChangeZapretBundleStatus(string status) => BeginInvoke(() => BundleStatusLabel.Text = $"Запрет-бандл: {status}");

        internal void ChangeDiscordDomainsCountLabel(int count) => BeginInvoke(() => DiscordDomainsCountLabel.Text = $"Число доменов Discord: {count}");

        internal void ChangeYouTubeDomainsCountLabel(int count) => BeginInvoke(() => YouTubeDomainsCountLabel.Text = $"Число доменов YouTube: {count}");

        internal void ChangeStatus(string status) => BeginInvoke(() => WorkStatusLabel.Text = status);

        private void MainButton_Click(object sender, EventArgs e)
        {
            if (!currentlyWorking)
            {
                if (ProcessManager.RunUnlocks())
                {
                    currentlyWorking = true;
                    ChangeStatus("работает :O");
                    (sender as Button).Text = "Остановить";
                }

            }
            else
            {
                ProcessManager.StopUnlocks();
                currentlyWorking = false;
                ChangeStatus("сейчас не работает :P");
                (sender as Button).Text = "Запустить";
            }
        }

        private void LauncherDevLabel_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/WutADude");
        }

        private void MainDevLabel_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/bol-van");
        }

        private void RepZapretLabel_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/bol-van/zapret");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentlyWorking)
                ProcessManager.StopUnlocks();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && HideInTrayCB.Checked)
            {
                if (notifIcon is null)
                    notifIcon = new NotifyIcon();
                notifIcon.BalloonTipText = "Спрячусь здесь!";
                notifIcon.Text = "Lazy DS & YT unlocker";
                notifIcon.Icon = Icon;
                notifIcon.Visible = true;
                notifIcon.ShowBalloonTip(500);
                notifIcon.DoubleClick += NotifIcon_DoubleClick;
                ShowInTaskbar = false;
                Hide();
            }
            else
            {
                if (notifIcon is not null)
                {
                    notifIcon.Visible = false;
                    ShowInTaskbar = true;
                    Show();
                }
            }
        }

        private void NotifIcon_DoubleClick(object? sender, EventArgs e)
        {
            notifIcon.Visible = false;
            ShowInTaskbar = true;
            Show();
        }
    }
}
