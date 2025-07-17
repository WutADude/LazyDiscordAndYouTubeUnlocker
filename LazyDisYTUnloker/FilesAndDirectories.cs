using LazyDisYTUnlocker.Properties;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;

namespace LazyDisYTUnlocker
{
    internal static class FilesAndDirectories
    {
        internal static MainForm Form { get; set; } = null!;

        internal const string WinwsPath = $"{_zapretDirectory}\\winws.exe";

        private const string _oldZapretDirectory = "zapret-win-bundle-master";
        private const string _oldWinwsDirectory = "zapret-winws";
        private static string _oldWinwsPath => $"{_oldZapretDirectory}\\{_oldWinwsDirectory}";
        private const string _zapretDirectory = "Zapret";
        private const string _strategiesDirectory = "Strategies";
        private const string _hostsDirectory = "Hosts";
        private const string _binarysDirPath = $"{_zapretDirectory}\\Bins";

        public static readonly Dictionary<string, string> PathsReplace = new Dictionary<string, string>()
        {
            { "[zapret]", _zapretDirectory },
            { "[bins]", Path.GetFullPath(_binarysDirPath) },
            { "[hosts]", Path.GetFullPath(_hostsDirectory) },
            { "[winwsdir]", _zapretDirectory }
        };

        internal static Dictionary<string, string> HostsPaths = new Dictionary<string, string>()
        {
            { "YouTubeHostsPath", $"{_hostsDirectory}\\YouTubeList.txt" },
            { "DiscordHostsPath", $"{_hostsDirectory}\\DiscordList.txt" },
            { "UserServicesHostsPath", $"{_hostsDirectory}\\UserServicesList.txt" }
        };

        internal static Dictionary<string, string> StrategiesPaths = new Dictionary<string, string>()
        {
            { "YouTubeStrategiesPath", $"{_strategiesDirectory}\\YouTubeStrats.txt" },
            { "DiscordStrategiesPath", $"{_strategiesDirectory}\\DiscordStrats.txt" },
            { "UserServicesStrategiesPath", $"{_strategiesDirectory}\\UserServicesStrats.txt" }
        };


        private static readonly List<string> _zapretFiles = new List<string>() { "winws.exe", "WinDivert.dll", "WinDivert64.sys", "cygwin1.dll" };

        internal static bool IsZapretBundleDirectoriesLoaded()
        {
            if (Directory.Exists(_oldZapretDirectory))
                ReinstallOldZapret();
            if (Directory.Exists("Zapret"))
            {
                Form.ChangeZapretBundleStatus(StringsLocalization.ZapretReadyToWorkStatus);
                SetupStrategyFilesWathcer();
                SetupHostsFilesWatcher();
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
                    if (Directory.Exists(_zapretDirectory))
                        Directory.Delete(_zapretDirectory, true);
                    Process.Start(new ProcessStartInfo() { FileName = "cmd.exe", Arguments = "/C sc delete Windivert", CreateNoWindow = true, UseShellExecute = false });
                }
                foreach (var dir in new string[] {_zapretDirectory, _binarysDirPath, _hostsDirectory, _strategiesDirectory })
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                using WebClient client = new WebClient();
                client.DownloadProgressChanged += (s, e) => Form.ChangeZapretBundleStatus(StringsLocalization.ZapretDownloadingProgress.Replace("%progress%", e.ProgressPercentage.ToString()));
                var data = await client.DownloadDataTaskAsync(new Uri("https://github.com/bol-van/zapret-win-bundle/archive/refs/heads/master.zip"));
                Form.ChangeZapretBundleStatus(StringsLocalization.ZapretUnpackingAndPreparing);
                UnzipZapret(data);
                Form.ChangeZapretBundleStatus(StringsLocalization.ZapretReadyToWorkStatus);
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
            File.WriteAllLines(HostsPaths["UserServicesHostsPath"], lines);
            Form.ChangeUserServicesDomainsCountLabel(GetLinesCount(HostsPaths["UserServicesHostsPath"]));
        }

        internal static string GetStrategy(string fullPath, int strategyIndex)
        {
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader streamReader = new StreamReader(fileStream))
                return streamReader.ReadToEnd().Trim().Split(ConfigManager.CurrentConfig.StrategiesSplitString)[strategyIndex];
        }

        private static async void ReinstallOldZapret()
        {
            Form.ChangeZapretBundleStatus(StringsLocalization.ZapretMovingStatus);
            foreach (var dir in new string[] { _zapretDirectory, _binarysDirPath, _hostsDirectory, _strategiesDirectory })
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            if (File.Exists($"{_oldWinwsPath}\\list-user_services.txt"))
                File.Move($"{_oldWinwsPath}\\list-user_services.txt", HostsPaths["UserServicesHostsPath"]);
            foreach (var file in new string[3]{"dsstrat.txt","ytstrat.txt","usstrat.txt"})
            {
                if (File.Exists(file))
                    File.Delete(file);
            }    
            if (Directory.Exists(_oldZapretDirectory))
                Directory.Delete(_oldZapretDirectory, true);
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
                    zapretEntry.ExtractToFile($"{_zapretDirectory}\\{file}");
                }
                List<ZipArchiveEntry> bins = entriesList.Where(e =>
                {
                    return e.Name.StartsWith("quic", StringComparison.OrdinalIgnoreCase) | e.Name.StartsWith("tls", StringComparison.OrdinalIgnoreCase) | e.Name.StartsWith("http", StringComparison.OrdinalIgnoreCase) | e.Name.StartsWith("dtls", StringComparison.OrdinalIgnoreCase) && e.Name.EndsWith("bin");
                }).ToList();
                if (bins.Count == 0)
                    throw new FileNotFoundException("Fake Quic or Tls or DTls files not found in archive!");
                foreach (var entry in bins)
                {
                    if (!File.Exists($"{_binarysDirPath}\\{entry.Name}"))
                        entry.ExtractToFile($"{_binarysDirPath}\\{entry.Name}");
                }
            }
        }

        private static void SetupHostsFilesWatcher()
        {
            FileSystemWatcher fileWatcher = new FileSystemWatcher()
            {
                Path = _hostsDirectory,
                Filter = "*.txt"
            };
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Changed += HostsFilesChanged;
            fileWatcher.EnableRaisingEvents = true;
        }

        private static void SetupStrategyFilesWathcer()
        {
            FileSystemWatcher fileWatcher = new FileSystemWatcher()
            {
                Path = _strategiesDirectory,
                Filter = "*.txt"
            };
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Changed += StrategyFilesChanged;
            fileWatcher.EnableRaisingEvents = true;
        }

        private static void StrategyFilesChanged(object sender, FileSystemEventArgs e)
        {
            if (e.Name is not null)
                Strategies.UpdateCounts(e.Name, GetLinesCount(e.FullPath, ConfigManager.CurrentConfig.StrategiesSplitString));
        }

        private static void HostsFilesChanged(object sender, FileSystemEventArgs e)
        {
            if (e.Name is not null)
                Strategies.UpdateCounts(e.Name, GetLinesCount(e.FullPath));
        }

        internal static int GetLinesCount(string fullPath, string? splitter = null)
        {
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader sr = new StreamReader(fileStream))
            {
                string lines = sr.ReadToEnd().Trim();
                if (string.IsNullOrWhiteSpace(lines))
                    return 0;
                if (string.IsNullOrWhiteSpace(splitter))
                    return lines.Split('\n').Length;
                return lines.Split(splitter).Length;
            }
        }

        internal static bool StrategiesAndHostsFilesExist
        {
           get 
           {
                foreach (var hosts in HostsPaths)
                    if (!File.Exists(hosts.Value))
                        return false;
                foreach (var strategies in StrategiesPaths)
                    if (!File.Exists(strategies.Value))
                        return false;
                return true;
           }
        }

        internal static string[] GetUserServicesDomainsLines { get => File.ReadAllLines(HostsPaths["UserServicesHostsPath"]); }

        internal static DateTime GetLatestStrategiesUpdateTime { get => File.GetLastWriteTime(Directory.GetFiles(_strategiesDirectory, "*.txt").OrderByDescending(f => new FileInfo(f).LastWriteTime).First()); }
    }
}
