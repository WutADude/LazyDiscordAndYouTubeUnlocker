using LazyDisYTUnlocker.Properties;

namespace LazyDisYTUnlocker
{
    internal static class Strategies
    {
        private static readonly string _strategiesSplitter = ">NEW_STRATEGY";

        internal static MainForm Form { get; set; } = null!;

        private static List<string> _youTubeStrategies { get; set; } = null!;
        private static List<string> _discordStrategies { get; set; } = null!;
        private static List<string> _userServicesStrategies { get; set; } = null!;

        internal static string DiscordStrategy { get => _discordStrategies[ConfigManager.CurrentConfig.ChoosenDiscordStrategy]; }
        internal static string YouTubeStrategy { get => _youTubeStrategies[ConfigManager.CurrentConfig.ChoosenYouTubeStrategy]; }
        internal static string UserServicesStrategy { get => _userServicesStrategies[ConfigManager.CurrentConfig.ChoosenUserServicesStrategy]; }

        internal static async Task<bool> GetStrategies(bool update)
        {
            try
            {
                Form.BeginInvoke(() =>
                {
                    Form.ChangeYTStrategyButton.Enabled = false;
                    Form.ChangeDSStrategyButton.Enabled = false;
                    Form.ChangeUserServicesStrategiesButton.Enabled = false;
                });
                if (!update)
                {
                    if (FilesAndDirectories.StrategiesAndHostsFilesExist)
                        return true;
                    return await GetStrategies(true);
                }
                using (HttpClient client = new HttpClient())
                {
                    File.WriteAllText(FilesAndDirectories.DiscordHostsFilePath, await client.GetStringAsync(DataURLs.DiscordHostsURL));
                    File.WriteAllText(FilesAndDirectories.YouTubeHostsFilePath, await client.GetStringAsync(DataURLs.YouTubeHostsURL));
                    File.WriteAllText(FilesAndDirectories.DiscordStrategiesFileName, await client.GetStringAsync(DataURLs.DiscordStrategiesURL));
                    File.WriteAllText(FilesAndDirectories.YouTubeStrategiesFileName, await client.GetStringAsync(DataURLs.YouTubeStrategiesURL));
                    File.WriteAllText(FilesAndDirectories.UserServicesStrategiesFileName, await client.GetStringAsync(DataURLs.UserServicesStrategiesURL));
                }
                if (!File.Exists(FilesAndDirectories.UserServicesHostsFilePath))
                    File.Create(FilesAndDirectories.UserServicesHostsFilePath).Close();
                ConfigManager.CurrentConfig.ChoosenYouTubeStrategy = 0;
                ConfigManager.CurrentConfig.ChoosenDiscordStrategy = 0;
                ConfigManager.CurrentConfig.ChoosenUserServicesStrategy = 0;
                ConfigManager.SaveConfig();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(StringsLocalization.StrategiesUpdateErrorMessageText.Replace("%error%", ex.Message), StringsLocalization.StrategiesUpdateErrorMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                _discordStrategies = new List<string>(File.ReadAllText(FilesAndDirectories.DiscordStrategiesFileName).Split(_strategiesSplitter));
                _youTubeStrategies = new List<string>(File.ReadAllText(FilesAndDirectories.YouTubeStrategiesFileName).Split(_strategiesSplitter));
                _userServicesStrategies = new List<string>(File.ReadAllText(FilesAndDirectories.UserServicesStrategiesFileName).Split(_strategiesSplitter));
                Form.ChangeLastStrategiesUpdateDate(File.GetLastWriteTime(Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt").OrderByDescending(f => new FileInfo(f).LastWriteTime).First()));
                Form.ChangeDiscordDomainsCountLabel(File.ReadAllLines(FilesAndDirectories.DiscordHostsFilePath).Length);
                Form.ChangeYouTubeDomainsCountLabel(File.ReadAllLines(FilesAndDirectories.YouTubeHostsFilePath).Length);
                Form.ChangeUserServicesDomainsCountLabel(File.ReadAllLines(FilesAndDirectories.UserServicesHostsFilePath).Length);
                Form.ChangeYTStrategiesLabel(_youTubeStrategies.Count, ConfigManager.CurrentConfig.ChoosenYouTubeStrategy);
                Form.ChangeDSStrategiesLabel(_discordStrategies.Count, ConfigManager.CurrentConfig.ChoosenDiscordStrategy);
                Form.ChangeUserServicesStrategiesLabel(_userServicesStrategies.Count, ConfigManager.CurrentConfig.ChoosenUserServicesStrategy);
                if (YTStrategiesCount > 1)
                    Form.BeginInvoke(() => Form.ChangeYTStrategyButton.Enabled = true);
                if (DSStrategiesCount > 1)
                    Form.BeginInvoke(() => Form.ChangeDSStrategyButton.Enabled = true);
                if (USStrategiesCount > 1)
                    Form.BeginInvoke(() => Form.ChangeUserServicesStrategiesButton.Enabled = true);
            }
        }

        internal static void ChangeStrategy(int type)
        {
            switch (type)
            {
                case 0:
                    if ((ConfigManager.CurrentConfig.ChoosenYouTubeStrategy + 1) < _youTubeStrategies.Count)
                        ConfigManager.CurrentConfig.ChoosenYouTubeStrategy++;
                    else
                        ConfigManager.CurrentConfig.ChoosenYouTubeStrategy = 0;
                    Form.ChangeYTStrategiesLabel(_youTubeStrategies.Count, ConfigManager.CurrentConfig.ChoosenYouTubeStrategy);
                    break;
                case 1:
                    if ((ConfigManager.CurrentConfig.ChoosenDiscordStrategy + 1) < _discordStrategies.Count)
                        ConfigManager.CurrentConfig.ChoosenDiscordStrategy++;
                    else
                        ConfigManager.CurrentConfig.ChoosenDiscordStrategy = 0;
                    Form.ChangeDSStrategiesLabel(_discordStrategies.Count, ConfigManager.CurrentConfig.ChoosenDiscordStrategy);
                    break;
                case 2:
                    if ((ConfigManager.CurrentConfig.ChoosenUserServicesStrategy + 1) < _userServicesStrategies.Count)
                        ConfigManager.CurrentConfig.ChoosenUserServicesStrategy++;
                    else
                        ConfigManager.CurrentConfig.ChoosenUserServicesStrategy = 0;
                    Form.ChangeUserServicesStrategiesLabel(_userServicesStrategies.Count, ConfigManager.CurrentConfig.ChoosenUserServicesStrategy);
                    break;
            }
            ConfigManager.SaveConfig();
        }

        internal static int YTStrategiesCount => _youTubeStrategies.Count;

        internal static int DSStrategiesCount => _discordStrategies.Count;

        internal static int USStrategiesCount => _userServicesStrategies.Count;
    }
}
