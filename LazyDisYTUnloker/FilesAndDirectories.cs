using LazyDisYTUnlocker.Properties;
using System.IO.Compression;
using System.Net;

namespace LazyDisYTUnlocker
{
    internal static class FilesAndDirectories
    {
        internal static MainForm Form { get; set; } = null!;

        internal const string MainZapretDirectory = "zapret-win-bundle-master";
        internal const string _winwsDirectory = "zapret-winws";

        internal static string YouTubeHostsFilePath { get => $"{GetWinwsPath}\\list-youtube.txt"; }
        internal static string DiscordHostsFilePath { get => $"{GetWinwsPath}\\list-discord.txt"; }
        internal static string UserServicesHostsFilePath { get => $"{GetWinwsPath}\\list-user_services.txt"; }

        internal const string DiscordStrategiesFileName = "dsstrat.txt";
        internal const string YouTubeStrategiesFileName = "ytstrat.txt";
        internal const string UserServicesStrategiesFileName = "usstrat.txt";

        internal static bool IsZapretBundleDirectoriesLoaded()
        {
            if (Directory.Exists(MainZapretDirectory))
            {
                Form.ChangeZapretBundleStatus(StringsLocalization.ZapretReadyToWorkStatus);
                return true;
            }
            Form.ChangeZapretBundleStatus(StringsLocalization.ZapretNotReadyToWorkStatus);
            if (Directory.Exists(MainZapretDirectory))
                Directory.Delete(MainZapretDirectory, true);
            return false;
        }

        internal static async Task<bool> DownloadUnpackAndSetupZapret()
        {
            try
            {
                using WebClient client = new WebClient();
                client.DownloadProgressChanged += (s, e) => Form.ChangeZapretBundleStatus(StringsLocalization.ZapretDownloadingProgress.Replace("%progress%", e.ProgressPercentage.ToString()));
                var data = await client.DownloadDataTaskAsync(new Uri("https://github.com/bol-van/zapret-win-bundle/archive/refs/heads/master.zip"));
                Form.ChangeZapretBundleStatus(StringsLocalization.ZapretUnpackingAndPreparing);
                using (MemoryStream ms = new MemoryStream(data))
                using (ZipArchive archive = new ZipArchive(ms, ZipArchiveMode.Read))
                    archive.ExtractToDirectory(Directory.GetCurrentDirectory());
                return true;
            }
            catch
            {
                Form.ChangeZapretBundleStatus(StringsLocalization.ZapretUnsuccessfullDownloadAndPrepare);
                return false;
            }
        }

        internal static void RewriteUserServicesHostsFile(IEnumerable<string> lines)
        {
            File.WriteAllLines(UserServicesHostsFilePath, lines);
            Form.ChangeUserServicesDomainsCountLabel(File.ReadAllLines(UserServicesHostsFilePath).Length);
        }

        internal static string GetWinwsPath => $"{MainZapretDirectory}\\{_winwsDirectory}";

        internal static bool StrategiesAndHostsFilesExist { get => File.Exists(DiscordStrategiesFileName) && File.Exists(YouTubeStrategiesFileName) && File.Exists(YouTubeHostsFilePath) && File.Exists(DiscordHostsFilePath) && File.Exists(UserServicesHostsFilePath); }

        internal static string[] GetUserServicesDomainsLines { get => File.ReadAllLines(UserServicesHostsFilePath); }
    }
}
