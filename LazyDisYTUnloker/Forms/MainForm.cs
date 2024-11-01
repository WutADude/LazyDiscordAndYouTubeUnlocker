using LazyDisYTUnlocker.Properties;
using LazyDisYTUnlocker.Forms;
using System.Diagnostics;

namespace LazyDisYTUnlocker
{
    public partial class MainForm : Form
    {
        private bool currentlyWorking = false;
        NotifyIcon? notifIcon { get; set; } = null;
        private InfoForm? InfoForm { get; set; }

        public MainForm()
        {
            InitializeComponent();
            (FilesAndDirectories.Form, Strategies.Form, ProcessManager.Form, Version.Form) = (this, this, this, this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckFilesAndSetup().ConfigureAwait(false);
        }

        private void MainButton_Click(object sender, EventArgs e)
        {
            if (!currentlyWorking)
            {
                if (ProcessManager.RunStrategies())
                {
                    currentlyWorking = true;
                    ChangeStatus(StringsLocalization.MainStatusEureekaWorking);
                    MainButton.Text = StringsLocalization.MainButtonStopText;
                    ChangeYTStrategyButton.Enabled = false;
                    ChangeDSStrategyButton.Enabled = false;
                }

            }
            else
            {
                ProcessManager.StopStrategies();
                currentlyWorking = false;
                ChangeStatus(StringsLocalization.MainStatusReadyToWork);
                MainButton.Text = StringsLocalization.MainButtonStartText;
                if (Strategies.YTStrategiesCount > 1)
                    ChangeYTStrategyButton.Enabled = true;
                if (Strategies.DSStrategiesCount > 1)
                    ChangeDSStrategyButton.Enabled = true;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentlyWorking)
                ProcessManager.StopStrategies();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && HideInTrayCB.Checked)
                HideInTray();
        }

        private void NotifIcon_DoubleClick(object? sender, EventArgs e) => RevealFromTray();

        private async void UpdateStrategiesButton_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                ChangeStatus(StringsLocalization.StrategiesUpdate);
                BeginInvoke(() =>
                {
                    MainButton.Enabled = false;
                    UpdateStrategiesButton.Enabled = false;
                });
                if (await Strategies.GetStrategies(true))
                {
                    BeginInvoke(() =>
                    {
                        MainButton.Enabled = true;
                        UpdateStrategiesButton.Enabled = true;
                    });
                    ChangeStatus(StringsLocalization.MainStatusReadyToWork);
                }
            });
        }

        private void ReinstallZapretLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => UpdateZapret().ConfigureAwait(false);

        private async Task CheckFilesAndSetup()
        {
            Task.Run(async () =>
            {
                BeginInvoke(() =>
                {
                    HideInTrayCB.Checked = ConfigManager.CurrentConfig.HideInTrayOnMinimize;
                    WindivertServiceCB.Checked = ConfigManager.CurrentConfig.KillWindivertOnStop;
                });
                if (await Version.IsNewVersionAvailable())
                    BeginInvoke(() => SoftwareVersionLabel.Font = new Font(SoftwareVersionLabel.Font, FontStyle.Underline));
                if (!FilesAndDirectories.IsZapretBundleDirectoriesLoaded())
                {
                    if (MessageBox.Show(StringsLocalization.ZapretLoadMessageText, StringsLocalization.ZapretLoadMessageCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ChangeStatus(StringsLocalization.DownloadFilesAndStrategiesStatus);
                        if (await FilesAndDirectories.DownloadUnpackAndSetupZapret() && FilesAndDirectories.IsZapretBundleDirectoriesLoaded() && await Strategies.GetStrategies(true))
                        {
                            ChangeZapretBundleStatus(StringsLocalization.ZapretReadyToWorkStatus);
                            BeginInvoke(() =>
                            {
                                MainButton.Enabled = true;
                                UpdateStrategiesButton.Enabled = true;
                            });
                            ChangeStatus(StringsLocalization.MainStatusReadyToWork);
                        }
                    }
                    else
                    {
                        MessageBox.Show(StringsLocalization.UserZapretInfoText, StringsLocalization.UserZapretInfoCaption);
                    }
                }
                else
                {
                    if (await Strategies.GetStrategies(false))
                    {
                        BeginInvoke(() =>
                        {
                            MainButton.Enabled = true;
                            UpdateStrategiesButton.Enabled = true;
                        });
                        ChangeStatus(StringsLocalization.MainStatusReadyToWork);
                    }
                }
            });
        }

        private async Task UpdateZapret()
        {
            Task.Run(async () =>
            {
                try
                {
                    if (currentlyWorking)
                        MainButton.PerformClick();
                    ProcessManager.KillWinDivertService();
                    BeginInvoke(() =>
                    {
                        ReinstallZapretLinkLabel.Enabled = false;
                        MainButton.Enabled = false;
                    });
                    if (Directory.Exists(FilesAndDirectories.MainZapretDirectory))
                        Directory.Delete(FilesAndDirectories.MainZapretDirectory, true);
                    ChangeStatus(StringsLocalization.ZapretUpdateStatus);
                    if (await FilesAndDirectories.DownloadUnpackAndSetupZapret() && FilesAndDirectories.IsZapretBundleDirectoriesLoaded())
                    {
                        BeginInvoke(() =>
                        {
                            MainButton.Enabled = true;
                            UpdateStrategiesButton.Enabled = true;
                            ReinstallZapretLinkLabel.Enabled = true;
                        });
                        ChangeStatus(StringsLocalization.MainStatusReadyToWork);
                        ChangeZapretBundleStatus(StringsLocalization.ZapretReadyToWorkStatus);
                    }
                    else
                    {
                        ChangeStatus(StringsLocalization.MainStatusSomethingWentWrong);
                        ChangeZapretBundleStatus(StringsLocalization.ZapretUnsuccessfullDownloadAndPrepare);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(StringsLocalization.ZapretUpdateErrorMessageText.Replace("%error%", ex.Message), StringsLocalization.ZapretUpdateErrorMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private void HideInTray()
        {
            notifIcon ??= new NotifyIcon();
            notifIcon.BalloonTipText = StringsLocalization.NotificonMessage;
            notifIcon.Text = "DS and YT unlock launcher";
            notifIcon.Icon = Icon;
            notifIcon.Visible = true;
            notifIcon.ShowBalloonTip(500);
            notifIcon.DoubleClick += NotifIcon_DoubleClick;
            ShowInTaskbar = false;
            Hide();
        }

        private void RevealFromTray()
        {
            notifIcon.Visible = false;
            ShowInTaskbar = true;
            Show();
        }

        private void SoftwareVersionLabel_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/WutADude/LazyDiscordAndYouTubeUnlocker/releases/latest");
        }

        private void SoftwareAboutLabel_Click(object sender, EventArgs e)
        {
            InfoForm ??= new InfoForm();
            InfoForm.ShowDialog(this);
        }

        private void ChangeYTStrategyButton_Click(object sender, EventArgs e) => Strategies.ChangeStrategy(0);

        private void ChangeDSStrategyButton_Click(object sender, EventArgs e) => Strategies.ChangeStrategy(1);

        internal void ChangeZapretBundleStatus(string status) => BeginInvoke(() => BundleStatusLabel.Text = StringsLocalization.ZapretStatusMain.Replace("%status%", status));

        internal void ChangeDiscordDomainsCountLabel(int count) => BeginInvoke(() => DiscordDomainsCountLabel.Text = StringsLocalization.DiscordDomainsCountLabel.Replace("%count%", count.ToString()));

        internal void ChangeYouTubeDomainsCountLabel(int count) => BeginInvoke(() => YouTubeDomainsCountLabel.Text = StringsLocalization.YouTubeDomainsCountLabel.Replace("%count%", count.ToString()));

        internal void ChangeStatus(string status) => BeginInvoke(() => SoftStatus.Text = status);

        internal void ChangeLastStrategiesUpdateDate(DateTime dateTime) => BeginInvoke(() => StrategiesUpdateDateLabel.Text =  StringsLocalization.StrategiesUpdateDateLabel.Replace("%date%", dateTime.ToString("HH:mm:ss dd.MM.yyyy")));

        internal void ChangeSoftwareVersionLabel(string currentVersion, string latestVersion) => BeginInvoke(() => SoftwareVersionLabel.Text = StringsLocalization.SoftwareVersionLabel.Replace("%current%", currentVersion).Replace("%latest%", latestVersion));

        internal void ChangeYTStrategiesLabel(int strategiesCount, int choosenStrategyIndex) => BeginInvoke(() => YTStrategiesCountLabel.Text = StringsLocalization.YouTubeStrategiesLabel.Replace("%available%", strategiesCount.ToString()).Replace("%selected%", (++choosenStrategyIndex).ToString()));

        internal void ChangeDSStrategiesLabel(int strategiesCount, int choosenStrategyIndex) => BeginInvoke(() => DSStrategiesLabel.Text = StringsLocalization.DiscordStrategiesLabel.Replace("%available%", strategiesCount.ToString()).Replace("%selected%", (++choosenStrategyIndex).ToString()));

        private void HideInTrayCB_Click(object sender, EventArgs e)
        {
            ConfigManager.CurrentConfig.HideInTrayOnMinimize = HideInTrayCB.Checked;
            ConfigManager.SaveConfig();
        }

        private void WindivertServiceCB_Click(object sender, EventArgs e)
        {
            ConfigManager.CurrentConfig.KillWindivertOnStop = WindivertServiceCB.Checked;
            ConfigManager.SaveConfig();
        }

        private void LocalizationLabel_Click(object sender, EventArgs e)
        {
            ConfigManager.CurrentConfig.ChoosenCulture = LocalizationLabel.Text;
            ConfigManager.SaveConfig();
            if (currentlyWorking)
                MainButton.PerformClick();
            Application.Restart();
        }
    }
}
