using LazyDisYTUnlocker.Properties;
using LazyDisYTUnlocker.Forms;
using System.Diagnostics;

namespace LazyDisYTUnlocker
{
    public partial class MainForm : Form
    {
        internal bool CurrentlyWorking = false;
        private NotifyIcon? _notifIcon { get; set; } = null;
        private InfoForm? _infoForm { get; set; }
        private DomainsAppenderForm? _userServicesDomainsForm { get; set; }

        private ContextMenuStrip _contextMenu = new ContextMenuStrip();
        private ToolStripButton _miniMainButton { get; set; } = null!;
        private ToolStripButton _exitButton { get; set; } = null!;

        private bool _isAutoRunning = false;

        public MainForm(string[] args)
        {
            InitializeComponent();
            (FilesAndDirectories.Form, Strategies.Form, ProcessManager.Form, Version.Form) = (this, this, this, this);
            _miniMainButton = new ToolStripButton((CurrentlyWorking ? StringsLocalization.MainButtonStopText : StringsLocalization.MainButtonStartText),
                null,
                onClick: (s, e) =>
                {
                    MainButton_Click(s!, e);
                    _miniMainButton.Text = CurrentlyWorking ? StringsLocalization.MainButtonStopText : StringsLocalization.MainButtonStartText;
                });
            _exitButton = new ToolStripButton("EXIT", null, onClick: (_, _) => Close());
            _contextMenu.Items.Add(_miniMainButton);
            _contextMenu.Items.Add(_exitButton);
            _isAutoRunning = args.Contains("-auto_run");
        }

        private void MainForm_Load(object sender, EventArgs e) => _ = CheckFilesAndSetup();

        private void MainButton_Click(object sender, EventArgs e)
        {
            if (!CurrentlyWorking)
            {
                if (ProcessManager.RunStrategies())
                {
                    CurrentlyWorking = true;
                    ChangeStatus(StringsLocalization.MainStatusEureekaWorking);
                    MainButton.Text = _miniMainButton.Text = StringsLocalization.MainButtonStopText;
                    ChangeYTStrategyButton.Enabled = ChangeDSStrategyButton.Enabled = ChangeUserServicesStrategiesButton.Enabled = UpdateHostsAndStrategiesButton.Enabled = false;
                }

            }
            else
            {
                ProcessManager.StopStrategies();
                CurrentlyWorking = false;
                ChangeStatus(StringsLocalization.MainStatusReadyToWork);
                MainButton.Text = _miniMainButton.Text = StringsLocalization.MainButtonStartText;
                ChangeYTStrategyButton.Enabled = Strategies.YTStrategiesCount > 1;
                ChangeDSStrategyButton.Enabled = Strategies.DSStrategiesCount > 1;
                ChangeUserServicesStrategiesButton.Enabled = Strategies.USStrategiesCount > 1;
                UpdateHostsAndStrategiesButton.Enabled = true;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CurrentlyWorking)
                ProcessManager.StopStrategies();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && HideInTrayCB.Checked)
                HideInTray();
        }

        private void NotifIcon_DoubleClick(object? sender, EventArgs e) => RevealFromTray();

        private void UpdateStrategiesButton_Click(object sender, EventArgs e)
        {
            _ = Strategies.UpdateStrategies();
        }

        private void ReinstallZapretLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => _ = UpdateZapret();

        private async Task CheckFilesAndSetup()
        {
            BeginInvoke(() =>
            {
                HideInTrayCB.Checked = ConfigManager.CurrentConfig.HideInTrayOnMinimize;
                WindivertServiceCB.Checked = ConfigManager.CurrentConfig.KillWindivertOnStop;
                AutoRunCB.Checked = ProcessManager.IsAutoRunTaskExists;
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
                            UpdateHostsAndStrategiesButton.Enabled = true;
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
                        UpdateHostsAndStrategiesButton.Enabled = true;
                    });
                    ChangeStatus(StringsLocalization.MainStatusReadyToWork);
                }
                if (_isAutoRunning)
                {
                    MainButton_Click(null, null);
                    WindowState = FormWindowState.Minimized;
                    HideInTray();
                }
            }
        }

        private async Task UpdateZapret()
        {
            try
            {
                if (CurrentlyWorking)
                    MainButton.PerformClick();
                ProcessManager.KillWinDivertService();
                BeginInvoke(() =>
                {
                    ReinstallZapretLinkLabel.Enabled = false;
                    MainButton.Enabled = false;
                });
                ChangeStatus(StringsLocalization.ZapretUpdateStatus);
                if (await FilesAndDirectories.DownloadUnpackAndSetupZapret(zapretUpdateOrReinstall: true) && FilesAndDirectories.IsZapretBundleDirectoriesLoaded())
                {
                    BeginInvoke(() =>
                    {
                        MainButton.Enabled = true;
                        UpdateHostsAndStrategiesButton.Enabled = true;
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
        }

        private void HideInTray()
        {
            _notifIcon ??= new NotifyIcon();
            _notifIcon.BalloonTipText = StringsLocalization.NotificonMessage;
            _notifIcon.Text = "LazyUnlocker";
            _notifIcon.Icon = Icon;
            _notifIcon.Visible = true;
            _notifIcon.ShowBalloonTip(500);
            _notifIcon.DoubleClick += NotifIcon_DoubleClick;
            _notifIcon.ContextMenuStrip = _contextMenu;
            ShowInTaskbar = false;
            Hide();
        }

        private void RevealFromTray()
        {
            _notifIcon!.Visible = false;
            ShowInTaskbar = true;
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void SoftwareVersionLabel_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/WutADude/LazyDiscordAndYouTubeUnlocker/releases/latest");
        }

        private void SoftwareAboutLabel_Click(object sender, EventArgs e)
        {
            _infoForm ??= new InfoForm();
            _infoForm.ShowDialog(this);
        }

        private void UserServicesDomainsCountLabel_Click(object sender, EventArgs e)
        {
            _userServicesDomainsForm ??= new DomainsAppenderForm();
            _userServicesDomainsForm.ShowDialog(this);
        }

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


        private void AutoRunCB_Click(object sender, EventArgs e)
        {
            if (!AutoRunCB.Checked)
                ProcessManager.DeleteAutoRunTask();
            else
                ProcessManager.CreateAutoRunTask();
        }

        private void LocalizationLabel_Click(object sender, EventArgs e)
        {
            ConfigManager.CurrentConfig.ChoosenCulture = LocalizationLabel.Text;
            ConfigManager.SaveConfig();
            if (CurrentlyWorking)
                MainButton.PerformClick();
            Application.Restart();
        }

        private void ChangeYTStrategyButton_Click(object sender, EventArgs e) => Strategies.ChangeStrategy(0);

        private void ChangeDSStrategyButton_Click(object sender, EventArgs e) => Strategies.ChangeStrategy(1);

        private void ChangeUserServicesStrategiesButton_Click(object sender, EventArgs e) => Strategies.ChangeStrategy(2);

        private void UserServicesDomainsCountLabel_MouseEnter(object sender, EventArgs e) => AddSymbolToLabel(sender);

        private void UserServicesDomainsCountLabel_MouseLeave(object sender, EventArgs e) => RemoveSymbolFromLabel(sender);

        internal void ChangeZapretBundleStatus(string status) => BeginInvoke(() => BundleStatusLabel.Text = StringsLocalization.ZapretStatusMain.Replace("%status%", status));

        internal void ChangeDiscordDomainsCountLabel(int count) => BeginInvoke(() => DiscordDomainsCountLabel.Text = StringsLocalization.DiscordDomainsCountLabel.Replace("%count%", count.ToString()));

        internal void ChangeYouTubeDomainsCountLabel(int count) => BeginInvoke(() => YouTubeDomainsCountLabel.Text = StringsLocalization.YouTubeDomainsCountLabel.Replace("%count%", count.ToString()));

        internal void ChangeUserServicesDomainsCountLabel(int count) => BeginInvoke(() => UserServicesDomainsCountLabel.Text = StringsLocalization.USDomainsCountLabel.Replace("%count%", count.ToString()));

        internal void ChangeStatus(string status) => BeginInvoke(() => SoftStatus.Text = status);

        internal void ChangeLastStrategiesUpdateDate(DateTime dateTime) => BeginInvoke(() => StrategiesUpdateDateLabel.Text = StringsLocalization.StrategiesUpdateDateLabel.Replace("%date%", dateTime.ToString("HH:mm:ss dd.MM.yyyy")));

        internal void ChangeSoftwareVersionLabel(string currentVersion, string latestVersion) => BeginInvoke(() => SoftwareVersionLabel.Text = StringsLocalization.SoftwareVersionLabel.Replace("%current%", currentVersion).Replace("%latest%", latestVersion));

        internal void ChangeYTStrategiesLabel(int strategiesCount, int choosenStrategyIndex) => BeginInvoke(() => YTStrategiesCountLabel.Text = StringsLocalization.YouTubeStrategiesLabel.Replace("%available%", strategiesCount.ToString()).Replace("%selected%", (++choosenStrategyIndex).ToString()));

        internal void ChangeDSStrategiesLabel(int strategiesCount, int choosenStrategyIndex) => BeginInvoke(() => DSStrategiesLabel.Text = StringsLocalization.DiscordStrategiesLabel.Replace("%available%", strategiesCount.ToString()).Replace("%selected%", (++choosenStrategyIndex).ToString()));

        internal void ChangeUserServicesStrategiesLabel(int strategiesCount, int choosenStrategy) => BeginInvoke(() => UserServicesStrategiesLabel.Text = StringsLocalization.USStrategiesCountLabel.Replace("%available%", strategiesCount.ToString()).Replace("%selected%", (++choosenStrategy).ToString()));

        private void AddSymbolToLabel(object sender) => (sender as Label)!.Text = "⚙️" + (sender as Label)!.Text;

        private void RemoveSymbolFromLabel(object sender) => (sender as Label)!.Text = (sender as Label)!.Text.Replace("⚙️", "");
    }
}
