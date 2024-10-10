namespace LazyDisYTUnloker
{
    internal static class Strategies
    {
        internal static string DiscordStrategy { get; private set; } = null!;
        internal static string YouTubeStrategy { get; private set; } = null!;

        internal static async Task<bool> UpdateStrategies(bool update)
        {
            try
            {
                if (!update)
                {
                    if (File.Exists("dsstrat.txt") && File.Exists("ytstrat.txt"))
                    {
                        DiscordStrategy = File.ReadAllText("dsstrat.txt");
                        YouTubeStrategy = File.ReadAllText("ytstrat.txt");
                        return true;
                    }
                    return await UpdateStrategies(true);
                }
                using (HttpClient client = new HttpClient())
                {
                    DiscordStrategy = await client.GetStringAsync("https://raw.githubusercontent.com/WutADude/LazyDiscordAndYouTubeUnlocker/refs/heads/master/Dscord%20Zapret%20strategy.txt");
                    YouTubeStrategy = await client.GetStringAsync("https://raw.githubusercontent.com/WutADude/LazyDiscordAndYouTubeUnlocker/refs/heads/master/YouTube%20Zapret%20strategy.txt");
                    File.WriteAllText("dsstrat.txt", DiscordStrategy);
                    File.WriteAllText("ytstrat.txt", YouTubeStrategy);
                }
                
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
                if (File.Exists("dsstrat.txt"))
                    FilesAndDirectories.Form.ChangeLastStrategiesUpdateDate(new FileInfo("dsstrat.txt").LastWriteTime);
            }
        }
    }
}
