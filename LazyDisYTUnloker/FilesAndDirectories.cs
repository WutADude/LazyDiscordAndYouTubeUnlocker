using System.Configuration;
using System.IO.Compression;
using System.Net;

namespace LazyDisYTUnloker
{
    internal static class FilesAndDirectories
    {
        internal static MainForm Form { private get; set; } = null!;

        internal const string MainZapretDirectory = "zapret-win-bundle-master";
        internal const string WinwsDirectory = "zapret-winws";
        private const string _discordDomainsListFile = "list-discord.txt";
        private const string _youtubeDomainsListFile = "list-youtube.txt";

        private static List<string> _discordDomains = new List<string>(ConfigurationManager.AppSettings["discordDomains"].Split('\n'));
        private static List<string> _youtubeDomains = new List<string>(ConfigurationManager.AppSettings["youtubeDomains"].Split('\n'));

        internal static void SetupDirectory()
        {
            Directory.CreateDirectory(MainZapretDirectory);
        }

        internal static bool IsZapretBundleDirectoriesLoaded()
        {
            Form.ChangeDiscordDomainsCountLabel(_discordDomains.Count);
            Form.ChangeYouTubeDomainsCountLabel(_youtubeDomains.Count);
            if (Directory.Exists(MainZapretDirectory) && Directory.GetDirectories(MainZapretDirectory).Count() == 5)
            {
                Form.ChangeZapretBundleStatus("готов к работе");
                return true;
            }
            Form.ChangeZapretBundleStatus("не загружен и не готов к работе");
            if (Directory.Exists(MainZapretDirectory))
                Directory.Delete(MainZapretDirectory, true);
            return false;
        }

        internal static async Task<bool> DownloadUnpackAndSetupZapret()
        {
            try
            {
                using WebClient client = new WebClient();
                client.DownloadProgressChanged += (s, e) => Form.ChangeZapretBundleStatus($"загружаю {e.ProgressPercentage}%");
                var data = await client.DownloadDataTaskAsync(new Uri("https://github.com/bol-van/zapret-win-bundle/archive/refs/heads/master.zip"));
                Form.ChangeZapretBundleStatus("распаковываю и подготавливаю...");
                using (MemoryStream ms = new MemoryStream(data))
                using (ZipArchive archive = new ZipArchive(ms, ZipArchiveMode.Read))
                    archive.ExtractToDirectory(Directory.GetCurrentDirectory());
                if (!File.Exists($"{MainZapretDirectory}\\{WinwsDirectory}\\{_discordDomainsListFile}"))
                    CreateDiscordDomainsFile();
                if (!File.Exists($"{MainZapretDirectory}\\{WinwsDirectory}\\{_youtubeDomainsListFile}"))
                    CreateYouTubeDomainsFile();
                return true;
            }
            catch
            {
                Form.ChangeZapretBundleStatus("не получилось загрузить или распаковать :(");
                return false;
            }
            
        }

        private static void CreateDiscordDomainsFile()
        {
            File.WriteAllLines($"{MainZapretDirectory}\\{WinwsDirectory}\\{_discordDomainsListFile}", _discordDomains);
        }

        private static void CreateYouTubeDomainsFile()
        {
            File.WriteAllLines($"{MainZapretDirectory}\\{WinwsDirectory}\\{_youtubeDomainsListFile}", _youtubeDomains);
        }
    }
}
