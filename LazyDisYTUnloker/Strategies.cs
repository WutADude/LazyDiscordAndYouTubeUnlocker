namespace LazyDisYTUnloker
{
    internal static class Strategies
    {
        private static readonly string _strategiesSplitter = ">NEW_STRATEGY";

        internal static MainForm Form { get; set; } = null!;

        private static List<string> _youTubeStrategies { get; set; } = null!;
        private static List<string> _discordStrategies { get; set; } = null!;

        internal static string DiscordStrategy { get => _discordStrategies[Properties.Settings.Default.ChoosenDiscordStrategy]; }
        internal static string YouTubeStrategy { get => _youTubeStrategies[Properties.Settings.Default.ChoosenYouTubeStrategy]; }

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
                Properties.Settings.Default.ChoosenYouTubeStrategy = 0;
                Properties.Settings.Default.ChoosenDiscordStrategy = 0;
                Properties.Settings.Default.Save();
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
                Form.ChangeYTStrategiesLabel(_youTubeStrategies.Count, Properties.Settings.Default.ChoosenYouTubeStrategy);
                Form.ChangeDSStrategiesLabel(_discordStrategies.Count, Properties.Settings.Default.ChoosenDiscordStrategy);
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
                    if ((Properties.Settings.Default.ChoosenYouTubeStrategy + 1) < _youTubeStrategies.Count)
                        Properties.Settings.Default.ChoosenYouTubeStrategy++;
                    else
                        Properties.Settings.Default.ChoosenYouTubeStrategy = 0;
                    Form.ChangeYTStrategiesLabel(_youTubeStrategies.Count, Properties.Settings.Default.ChoosenYouTubeStrategy);
                    break;
                case 1:
                    if ((Properties.Settings.Default.ChoosenDiscordStrategy + 1) < _discordStrategies.Count)
                        Properties.Settings.Default.ChoosenDiscordStrategy++;
                    else
                        Properties.Settings.Default.ChoosenDiscordStrategy = 0;
                    Form.ChangeDSStrategiesLabel(_discordStrategies.Count, Properties.Settings.Default.ChoosenDiscordStrategy);
                    break;
            }
            Properties.Settings.Default.Save();
        }
    }
}
