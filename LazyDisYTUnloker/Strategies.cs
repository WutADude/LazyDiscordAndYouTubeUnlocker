namespace LazyDisYTUnloker
{
    internal static class Strategies
    {
        private static readonly string _strategiesSplitter = ">NEW_STRATEGY";

        internal static MainForm Form { get; set; } = null!;

        private static List<string> _youTubeStrategies { get; set; } = null!;
        private static List<string> _discordStrategies { get; set; } = null!;

        internal static string DiscordStrategy { get => _discordStrategies[ConfigManager.CurrentConfig.ChoosenDiscordStrategy]; }
        internal static string YouTubeStrategy { get => _youTubeStrategies[ConfigManager.CurrentConfig.ChoosenYouTubeStrategy]; }

        internal static async Task<bool> GetStrategies(bool update)
        {
            try
            {
                Form.BeginInvoke(() =>
                {
                    Form.ChangeYTStrategyButton.Enabled = false;
                    Form.ChangeDSStrategyButton.Enabled = false;
                });
                if (!update)
                {
                    if (File.Exists("dsstrat.txt") && File.Exists("ytstrat.txt") && File.Exists($"{FilesAndDirectories.GetWinwsPath}\\list-youtube.txt") && File.Exists($"{FilesAndDirectories.GetWinwsPath}\\list-discord.txt"))
                        return true;
                    return await GetStrategies(true);
                }
                using (HttpClient client = new HttpClient())
                {
                    File.WriteAllText($"{FilesAndDirectories.GetWinwsPath}\\list-discord.txt", await client.GetStringAsync(DataURLs.DiscordHostsURL));
                    File.WriteAllText($"{FilesAndDirectories.GetWinwsPath}\\list-youtube.txt", await client.GetStringAsync(DataURLs.YouTubeHostsURL));
                    File.WriteAllText("dsstrat.txt", await client.GetStringAsync(DataURLs.DiscordStrategiesURL));
                    File.WriteAllText("ytstrat.txt", await client.GetStringAsync(DataURLs.YouTubeStrategiesURL));
                }
                ConfigManager.CurrentConfig.ChoosenYouTubeStrategy = 0;
                ConfigManager.CurrentConfig.ChoosenDiscordStrategy = 0;
                ConfigManager.SaveConfig();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При получении/обновлении стратегий обхода произошла ошибка: \n\n" +
                    $"" +
                    $"{ex.Message}", "Ошибка при получении/обновлении стратегий", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                _discordStrategies = new List<string>(File.ReadAllText("dsstrat.txt").Split(_strategiesSplitter));
                _youTubeStrategies = new List<string>(File.ReadAllText("ytstrat.txt").Split(_strategiesSplitter));
                Form.ChangeLastStrategiesUpdateDate(new FileInfo("dsstrat.txt").LastWriteTime);
                Form.ChangeDiscordDomainsCountLabel(File.ReadAllLines($"{FilesAndDirectories.GetWinwsPath}\\list-discord.txt").Count());
                Form.ChangeYouTubeDomainsCountLabel(File.ReadAllLines($"{FilesAndDirectories.GetWinwsPath}\\list-youtube.txt").Count());
                Form.ChangeYTStrategiesLabel(_youTubeStrategies.Count, ConfigManager.CurrentConfig.ChoosenYouTubeStrategy);
                Form.ChangeDSStrategiesLabel(_discordStrategies.Count, ConfigManager.CurrentConfig.ChoosenDiscordStrategy);
                if (_youTubeStrategies.Count > 1)
                    Form.BeginInvoke(() => Form.ChangeYTStrategyButton.Enabled = true);
                if (_discordStrategies.Count > 1)
                    Form.BeginInvoke(() => Form.ChangeDSStrategyButton.Enabled = true);
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
            }
            ConfigManager.SaveConfig();
        }

        internal static int YTStrategiesCount => _youTubeStrategies.Count;

        internal static int DSStrategiesCount => _discordStrategies.Count;
    }
}
