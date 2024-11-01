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

        internal static bool IsZapretBundleDirectoriesLoaded()
        {
            if (Directory.Exists(MainZapretDirectory) && Directory.GetDirectories(MainZapretDirectory).Length == 5)
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

        internal static string GetWinwsPath => $"{MainZapretDirectory}\\{_winwsDirectory}";
    }
}
