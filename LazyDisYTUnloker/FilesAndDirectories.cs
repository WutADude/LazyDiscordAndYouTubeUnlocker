using LazyDisYTUnlocker.Properties;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;

namespace LazyDisYTUnlocker
{
    internal static class FilesAndDirectories
    {
        internal static MainForm Form { get; set; } = null!;

        internal const string OldZapretDirectory = "zapret-win-bundle-master";
        internal const string OldWinwsDirectory = "zapret-winws";
        internal static string OldWinwsPath => $"{OldZapretDirectory}\\{OldWinwsDirectory}";

        internal const string NewZapretDirectory = "Zapret";
        internal const string StrategiesDirectory = $"Strategies";
        internal const string HostsDirectory = $"Hosts";
        internal const string BinarysDirPath = $"{NewZapretDirectory}\\Bins";
        internal const string WinwsPath = $"{NewZapretDirectory}\\winws.exe";
        

        internal static string YouTubeHostsFilePath { get => $"{HostsDirectory}\\YouTubeList.txt"; }
        internal static string DiscordHostsFilePath { get => $"{HostsDirectory}\\DiscordList.txt"; }
        internal static string UserServicesHostsFilePath { get => $"{HostsDirectory}\\UserServicesList.txt"; }

        internal const string DiscordStrategiesPath = $"{StrategiesDirectory}\\DiscordStrats.txt";
        internal const string YouTubeStrategiesPath = $"{StrategiesDirectory}\\YouTubeStrats.txt";
        internal const string UserServicesStrategiesPAth = $"{StrategiesDirectory}\\UserServicesStrats.txt";

        private static readonly List<string> _zapretFiles = new List<string>() { "winws.exe", "WinDivert.dll", "WinDivert64.sys", "cygwin1.dll" };

        internal static bool IsZapretBundleDirectoriesLoaded()
        {
            if (Directory.Exists(OldZapretDirectory))
                ReinstallOldZapret();
            if (Directory.Exists("Zapret"))
            {
                Form.ChangeZapretBundleStatus(StringsLocalization.ZapretReadyToWorkStatus);
                return true;
            }
            Form.ChangeZapretBundleStatus(StringsLocalization.ZapretNotReadyToWorkStatus);
            return false;
        }

        internal static async Task<bool> DownloadUnpackAndSetupZapret(bool zapretUpdateOrReinstall = false)
        {
            try
            {
                if (zapretUpdateOrReinstall)
                {
                    if (Directory.Exists(NewZapretDirectory))
                        Directory.Delete(NewZapretDirectory, true);
                    Process.Start(new ProcessStartInfo() { FileName = "cmd.exe", Arguments = "/C sc delete Windivert", CreateNoWindow = true, UseShellExecute = false });
                }
                if (!Directory.Exists(NewZapretDirectory))
                    Directory.CreateDirectory(NewZapretDirectory);
                if (!Directory.Exists(BinarysDirPath))
                    Directory.CreateDirectory(BinarysDirPath);
                if (!Directory.Exists(HostsDirectory))
                    Directory.CreateDirectory(HostsDirectory);
                if (!Directory.Exists(StrategiesDirectory))
                    Directory.CreateDirectory(StrategiesDirectory);
                using WebClient client = new WebClient();
                client.DownloadProgressChanged += (s, e) => Form.ChangeZapretBundleStatus(StringsLocalization.ZapretDownloadingProgress.Replace("%progress%", e.ProgressPercentage.ToString()));
                var data = await client.DownloadDataTaskAsync(new Uri("https://github.com/bol-van/zapret-win-bundle/archive/refs/heads/master.zip"));
                Form.ChangeZapretBundleStatus(StringsLocalization.ZapretUnpackingAndPreparing);
                UnzipZapret(data);
                return true;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "UnzipError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

        private static async void ReinstallOldZapret()
        {
            Form.ChangeZapretBundleStatus(StringsLocalization.ZapretMovingStatus);
            if (!Directory.Exists(NewZapretDirectory))
                Directory.CreateDirectory(NewZapretDirectory);
            if (!Directory.Exists(BinarysDirPath))
                Directory.CreateDirectory(BinarysDirPath);
            if (!Directory.Exists(HostsDirectory))
                Directory.CreateDirectory(HostsDirectory);
            if (!Directory.Exists(StrategiesDirectory))
                Directory.CreateDirectory(StrategiesDirectory);
            if (File.Exists($"{OldWinwsPath}\\list-user_services.txt"))
                File.Move($"{OldWinwsPath}\\list-user_services.txt", $"{UserServicesHostsFilePath}");
            if (Directory.Exists(OldZapretDirectory))
                Directory.Delete(OldZapretDirectory, true);
            await DownloadUnpackAndSetupZapret();
        }

        private static void UnzipZapret(byte[]? zipBytes)
        {
            using (MemoryStream ms = new MemoryStream(zipBytes))
            using (ZipArchive archive = new ZipArchive(ms, ZipArchiveMode.Read))
            {
                var entriesList = archive.Entries.ToList();
                foreach (string file in _zapretFiles)
                {
                    ZipArchiveEntry? zapretEntry = entriesList.Where(e => e.Name.Equals(file, StringComparison.OrdinalIgnoreCase) && e.FullName.Contains("winws")).FirstOrDefault();
                    if (zapretEntry is null)
                        throw new FileNotFoundException($"{file} not found in archive!");
                    zapretEntry.ExtractToFile($"{NewZapretDirectory}\\{file}");
                }
                List<ZipArchiveEntry> bins = entriesList.Where(e =>
                {
                    return e.Name.StartsWith("quic", StringComparison.OrdinalIgnoreCase) | e.Name.StartsWith("tls", StringComparison.OrdinalIgnoreCase) | e.Name.StartsWith("http", StringComparison.OrdinalIgnoreCase) | e.Name.StartsWith("dtls", StringComparison.OrdinalIgnoreCase) && e.Name.EndsWith("bin");
                }).ToList();
                if (bins.Count == 0)
                    throw new FileNotFoundException("Fake Quic or Tls or DTls files not found in archive!");
                foreach (var entry in bins)
                {
                    if (!File.Exists($"{BinarysDirPath}\\{entry.Name}"))
                        entry.ExtractToFile($"{BinarysDirPath}\\{entry.Name}");
                }
            }
        }

        internal static bool StrategiesAndHostsFilesExist { get => File.Exists(DiscordStrategiesPath) && File.Exists(YouTubeStrategiesPath) && File.Exists(YouTubeHostsFilePath) && File.Exists(DiscordHostsFilePath) && File.Exists(UserServicesHostsFilePath); }

        internal static string[] GetUserServicesDomainsLines { get => File.ReadAllLines(UserServicesHostsFilePath); }
    }
}
