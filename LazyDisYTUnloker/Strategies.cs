namespace LazyDisYTUnloker
{
    internal static class Strategies
    {
        internal static string DiscordStrategy { get; private set; } = null!;
        internal static string YouTubeStrategy { get; private set; } = null!;

        internal static async Task<bool> GetStrategies(bool update)
        {
            try
            {
                if (!update)
                {
                    if (File.Exists("dsstrat.txt") && File.Exists("ytstrat.txt") && File.Exists($"{FilesAndDirectories.GetWinwsPath}\\list-youtube.txt") && File.Exists($"{FilesAndDirectories.GetWinwsPath}\\list-discord.txt"))
                    {
                        DiscordStrategy = File.ReadAllText("dsstrat.txt");
                        YouTubeStrategy = File.ReadAllText("ytstrat.txt");
                        FilesAndDirectories.Form.ChangeLastStrategiesUpdateDate(new FileInfo("dsstrat.txt").LastWriteTime);
                        FilesAndDirectories.Form.ChangeDiscordDomainsCountLabel(File.ReadAllLines($"{FilesAndDirectories.GetWinwsPath}\\list-discord.txt").Count());
                        FilesAndDirectories.Form.ChangeYouTubeDomainsCountLabel(File.ReadAllLines($"{FilesAndDirectories.GetWinwsPath}\\list-youtube.txt").Count());
                        return true;
                    }
                    return await GetStrategies(true);
                }
                using (HttpClient client = new HttpClient())
                {
                    DiscordStrategy = await client.GetStringAsync("https://raw.githubusercontent.com/WutADude/LazyDiscordAndYouTubeUnlocker/refs/heads/master/Strategies/Dscord%20Zapret%20strategy.txt");
                    YouTubeStrategy = await client.GetStringAsync("https://raw.githubusercontent.com/WutADude/LazyDiscordAndYouTubeUnlocker/refs/heads/master/Strategies/YouTube%20Zapret%20strategy.txt");
                    File.WriteAllText($"{FilesAndDirectories.GetWinwsPath}\\list-discord.txt", await client.GetStringAsync("https://raw.githubusercontent.com/WutADude/LazyDiscordAndYouTubeUnlocker/refs/heads/master/Hosts/DiscordHosts.txt")); //TODO: Допилить парс хостов
                    File.WriteAllText($"{FilesAndDirectories.GetWinwsPath}\\list-youtube.txt", await client.GetStringAsync("https://raw.githubusercontent.com/WutADude/LazyDiscordAndYouTubeUnlocker/refs/heads/master/Hosts/YouTubeHosts.txt"));
                    File.WriteAllText("dsstrat.txt", DiscordStrategy);
                    File.WriteAllText("ytstrat.txt", YouTubeStrategy);
                    
                }
                FilesAndDirectories.Form.ChangeLastStrategiesUpdateDate(new FileInfo("dsstrat.txt").LastWriteTime);
                FilesAndDirectories.Form.ChangeDiscordDomainsCountLabel(File.ReadAllLines($"{FilesAndDirectories.GetWinwsPath}\\list-discord.txt").Count());
                FilesAndDirectories.Form.ChangeYouTubeDomainsCountLabel(File.ReadAllLines($"{FilesAndDirectories.GetWinwsPath}\\list-youtube.txt").Count());
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При получении/обновлении стратегий обхода произошла ошибка: \n\n" +
                    $"" +
                    $"{ex.Message}", "Ошибка при получении/обновлении стратегий", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
