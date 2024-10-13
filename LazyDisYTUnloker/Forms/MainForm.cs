using LazyDisYTUnloker.Forms;
using System.Diagnostics;

namespace LazyDisYTUnloker
{
    public partial class MainForm : Form
    {
        private bool currentlyWorking = false;
        NotifyIcon? notifIcon { get; set; } = null;
        private InfoForm? InfoForm { get; set; }

        public MainForm()
        {
            InitializeComponent();
            ConfigManager.SetupConfig();
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
                    ChangeStatus("работает :O");
                    MainButton.Text = "Остановить";
                    ChangeYTStrategyButton.Enabled = false;
                    ChangeDSStrategyButton.Enabled = false;
                }

            }
            else
            {
                ProcessManager.StopStrategies();
                currentlyWorking = false;
                ChangeStatus("готов к работе");
                MainButton.Text = "Запустить";
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
                ChangeStatus("обновляю стратегии...");
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
                    ChangeStatus("готов к работе");
                }
            });
        }

        private void ReinstallZapretLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => UpdateZapret().ConfigureAwait(false);

        private async Task CheckFilesAndSetup()
        {
            Task.Run(async () =>
            {
                if (await Version.IsNewVersionAvailable())
                    BeginInvoke(() => SoftwareVersionLabel.Font = new Font(SoftwareVersionLabel.Font, FontStyle.Underline));
                if (!FilesAndDirectories.IsZapretBundleDirectoriesLoaded())
                {
                    if (MessageBox.Show("Для работы нужно догрузить \"Zapret\" и подготовить кое какие файлы, делаем?", "Чего-то не хватает, но чего?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ChangeStatus("скачиваю/проверяю файлы и обновляю стратегии...");
                        if (await FilesAndDirectories.DownloadUnpackAndSetupZapret() && FilesAndDirectories.IsZapretBundleDirectoriesLoaded() && await Strategies.GetStrategies(true))
                        {
                            ChangeZapretBundleStatus("готов к работе");
                            BeginInvoke(() =>
                            {
                                MainButton.Enabled = true;
                                UpdateStrategiesButton.Enabled = true;
                            });
                            ChangeStatus("готов к работе");
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
                {
                    if (await Strategies.GetStrategies(false))
                    {
                        BeginInvoke(() =>
                        {
                            MainButton.Enabled = true;
                            UpdateStrategiesButton.Enabled = true;
                        });
                        ChangeStatus("готов к работе");
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
                    ChangeStatus("скачиваю/проверяю файлы...");
                    if (await FilesAndDirectories.DownloadUnpackAndSetupZapret() && FilesAndDirectories.IsZapretBundleDirectoriesLoaded())
                    {
                        BeginInvoke(() =>
                        {
                            MainButton.Enabled = true;
                            UpdateStrategiesButton.Enabled = true;
                            ReinstallZapretLinkLabel.Enabled = true;
                        });
                        ChangeStatus("готов к работе");
                        ChangeZapretBundleStatus("готов к работе");
                    }
                    else
                    {
                        ChangeStatus("не удалось загрузить/распаковать Zapret");
                        ChangeZapretBundleStatus("что-то не то...");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось обновить Zapret!\n\n" +
                        "" +
                        $"Ошибка: {ex.Message}", "Так, обновления не будет!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private void HideInTray()
        {
            notifIcon ??= new NotifyIcon();
            notifIcon.BalloonTipText = "Спрячусь здесь!";
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

        internal void ChangeZapretBundleStatus(string status) => BeginInvoke(() => BundleStatusLabel.Text = $"Состояние Zapret: {status}");

        internal void ChangeDiscordDomainsCountLabel(int count) => BeginInvoke(() => DiscordDomainsCountLabel.Text = $"Число доменов Discord: {count}");

        internal void ChangeYouTubeDomainsCountLabel(int count) => BeginInvoke(() => YouTubeDomainsCountLabel.Text = $"Число доменов YouTube: {count}");

        internal void ChangeStatus(string status) => BeginInvoke(() => SoftStatus.Text = status);

        internal void ChangeLastStrategiesUpdateDate(DateTime dateTime) => BeginInvoke(() => StrategiesUpdateDateLabel.Text = $"Дата последнего обновления стратегий: {dateTime.ToString("HH:mm:ss dd.MM.yyyy")}");

        internal void ChangeSoftwareVersionLabel(string currentVersion, string latestVersion) => BeginInvoke(() => SoftwareVersionLabel.Text = $"Текущая версия: {currentVersion} (актуальная: {latestVersion})");

        internal void ChangeYTStrategiesLabel(int strategiesCount, int choosenStrategyIndex) => BeginInvoke(() => YTStrategiesCountLabel.Text = $"Число доступных стратегий YouTube: {strategiesCount} | Выбранная стратегия: №{++choosenStrategyIndex}");

        internal void ChangeDSStrategiesLabel(int strategiesCount, int choosenStrategyIndex) => BeginInvoke(() => DSStrategiesLabel.Text = $"Число доступных стратегий Discord: {strategiesCount} | Выбранная стратегия: №{++choosenStrategyIndex}");

        
    }
}
