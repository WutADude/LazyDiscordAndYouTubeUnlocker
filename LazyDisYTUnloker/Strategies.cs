using System;
using System.Collections.Generic;
using System.Linq;
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
                    try
                    {
                        DiscordStrategy = File.ReadAllText("dsstrat.txt");
                        YouTubeStrategy = File.ReadAllText("ytstrat.txt");
                        return true;
                    }
                    catch 
                    { 
                        return await UpdateStrategies(true);
                    }
                }
                using (HttpClient client = new HttpClient())
                {
                    DiscordStrategy = await client.GetStringAsync("https://pastebin.com/raw/QwT7SDkW");
                    YouTubeStrategy = await client.GetStringAsync("https://pastebin.com/raw/jwMKg0pe");
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
        }
    }
}
