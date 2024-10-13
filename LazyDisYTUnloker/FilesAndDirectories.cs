using System.Configuration;
using System.IO.Compression;
using System.Net;

namespace LazyDisYTUnloker
{
    internal static class FilesAndDirectories
    {
        internal static MainForm Form { get; set; } = null!;

        internal const string _mainZapretDirectory = "zapret-win-bundle-master";
        internal const string _winwsDirectory = "zapret-winws";

        internal static bool IsZapretBundleDirectoriesLoaded()
        {
            if (Directory.Exists(_mainZapretDirectory) && Directory.GetDirectories(_mainZapretDirectory).Count() == 5)
            {
                Form.ChangeZapretBundleStatus("готов к работе");
                return true;
            }
            Form.ChangeZapretBundleStatus("не загружен и не готов к работе");
            if (Directory.Exists(_mainZapretDirectory))
                Directory.Delete(_mainZapretDirectory, true);
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
                return true;
            }
            catch
            {
                Form.ChangeZapretBundleStatus("не получилось загрузить или распаковать Zapret :(");
                return false;
            }
        }

        internal static string GetWinwsPath => $"{_mainZapretDirectory}\\{_winwsDirectory}";
    }
}
