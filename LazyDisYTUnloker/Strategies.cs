using LazyDisYTUnlocker.Properties;
using System.Text;
using System.Text.RegularExpressions;

namespace LazyDisYTUnlocker
{
    internal static class Strategies
    {
        internal static MainForm Form { get; set; } = null!;

        private static int _ytStratsCount = 0;
        private static int _discordStratsCount = 0;
        private static int _userStratsCount = 0;

        internal static string DiscordStrategy { get => RegexManager.ReplacementRegex.Replace(FilesAndDirectories.GetStrategy(FilesAndDirectories.StrategiesPaths["DiscordStrategiesPath"], ConfigManager.CurrentConfig.ChoosenDiscordStrategy).Trim(), match => FilesAndDirectories.PathsReplace[match.Value]); }
        internal static string YouTubeStrategy { get => RegexManager.ReplacementRegex.Replace(FilesAndDirectories.GetStrategy(FilesAndDirectories.StrategiesPaths["YouTubeStrategiesPath"], ConfigManager.CurrentConfig.ChoosenYouTubeStrategy).Trim(), match => FilesAndDirectories.PathsReplace[match.Value]); }
        internal static string UserServicesStrategy { get => RegexManager.ReplacementRegex.Replace(FilesAndDirectories.GetStrategy(FilesAndDirectories.StrategiesPaths["UserServicesStrategiesPath"], ConfigManager.CurrentConfig.ChoosenUserServicesStrategy).Trim(), match => FilesAndDirectories.PathsReplace[match.Value]); }
        internal static string FinalStrategy { get => GetOneFullStrategy; }

        internal static int YTStrategiesCount => _ytStratsCount;
        internal static int DSStrategiesCount => _discordStratsCount;
        internal static int USStrategiesCount => _userStratsCount;

        internal static async Task<bool> GetStrategies(bool update)
        {
            try
            {
                Form.BeginInvoke(() =>
                {
                    Form.ChangeYTStrategyButton.Enabled = Form.ChangeDSStrategyButton.Enabled = Form.ChangeUserServicesStrategiesButton.Enabled = false;
                });
                if (!update)
                {
                    if (FilesAndDirectories.StrategiesAndHostsFilesExist)
                        return true;
                    return await GetStrategies(true);
                }
                using (HttpClient client = new HttpClient())
                {
                    foreach (var hosts in DataURLs.HostsAndUrls)
                        File.WriteAllText(hosts.Key, await client.GetStringAsync(hosts.Value));
                    foreach (var strategies in DataURLs.StrategiesAndUrls)
                        File.WriteAllText(strategies.Key, await client.GetStringAsync(strategies.Value));
                }
                if (!File.Exists(FilesAndDirectories.HostsPaths["UserServicesHostsPath"]))
                    File.Create(FilesAndDirectories.HostsPaths["UserServicesHostsPath"]).Close();
                ConfigManager.ResetChoosenStrategies();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(StringsLocalization.StrategiesUpdateErrorMessageText.Replace("%error%", ex.Message), StringsLocalization.StrategiesUpdateErrorMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {

                Form.ChangeLastStrategiesUpdateDate(FilesAndDirectories.GetLatestStrategiesUpdateTime);
                foreach (var hosts in FilesAndDirectories.HostsPaths)
                    UpdateCounts(Path.GetFileName(hosts.Value), FilesAndDirectories.GetLinesCount(hosts.Value));
                foreach (var strategies in FilesAndDirectories.StrategiesPaths)
                    UpdateCounts(Path.GetFileName(strategies.Value), FilesAndDirectories.GetLinesCount(strategies.Value, ConfigManager.CurrentConfig.StrategiesSplitString));
                Form.BeginInvoke(() =>
                {
                    Form.ChangeYTStrategyButton.Enabled = YTStrategiesCount > 1;
                    Form.ChangeDSStrategyButton.Enabled = DSStrategiesCount > 1;
                    Form.ChangeUserServicesStrategiesButton.Enabled = USStrategiesCount > 1;
                });
            }
        }

        internal static void ChangeStrategy(int type)
        {
            switch (type)
            {
                case 0:
                    if ((ConfigManager.CurrentConfig.ChoosenYouTubeStrategy + 1) < _ytStratsCount)
                        ConfigManager.CurrentConfig.ChoosenYouTubeStrategy++;
                    else
                        ConfigManager.CurrentConfig.ChoosenYouTubeStrategy = 0;
                    Form.ChangeYTStrategiesLabel(_ytStratsCount, ConfigManager.CurrentConfig.ChoosenYouTubeStrategy);
                    break;
                case 1:
                    if ((ConfigManager.CurrentConfig.ChoosenDiscordStrategy + 1) < _discordStratsCount)
                        ConfigManager.CurrentConfig.ChoosenDiscordStrategy++;
                    else
                        ConfigManager.CurrentConfig.ChoosenDiscordStrategy = 0;
                    Form.ChangeDSStrategiesLabel(_discordStratsCount, ConfigManager.CurrentConfig.ChoosenDiscordStrategy);
                    break;
                case 2:
                    if ((ConfigManager.CurrentConfig.ChoosenUserServicesStrategy + 1) < _userStratsCount)
                        ConfigManager.CurrentConfig.ChoosenUserServicesStrategy++;
                    else
                        ConfigManager.CurrentConfig.ChoosenUserServicesStrategy = 0;
                    Form.ChangeUserServicesStrategiesLabel(_userStratsCount, ConfigManager.CurrentConfig.ChoosenUserServicesStrategy);
                    break;
            }
            ConfigManager.SaveConfig();
        }

        internal static async Task UpdateStrategies()
        {
            Form.ChangeStatus(StringsLocalization.StrategiesUpdate);
            Form.BeginInvoke(() =>
            {
                Form.MainButton.Enabled = false;
                Form.UpdateHostsAndStrategiesButton.Enabled = false;
            });
            if (await GetStrategies(true))
            {
                Form.BeginInvoke(() =>
                {
                    Form.MainButton.Enabled = true;
                    Form.UpdateHostsAndStrategiesButton.Enabled = true;
                });
                Form.ChangeStatus(StringsLocalization.MainStatusReadyToWork);
            }
        }

        

        internal static void UpdateCounts(string fileName, int count)
        {
            switch (fileName)
            {
                case "YouTubeStrats.txt":
                    _ytStratsCount = count;
                    Form.ChangeYTStrategiesLabel(_ytStratsCount, ConfigManager.CurrentConfig.ChoosenYouTubeStrategy);
                    break;
                case "DiscordStrats.txt":
                    _discordStratsCount = count;
                    Form.ChangeDSStrategiesLabel(_discordStratsCount, ConfigManager.CurrentConfig.ChoosenDiscordStrategy);
                    break;
                case "UserServicesStrats.txt":
                    _userStratsCount = count;
                    Form.ChangeUserServicesStrategiesLabel(_userStratsCount, ConfigManager.CurrentConfig.ChoosenUserServicesStrategy);
                    break;
                case "YouTubeList.txt":
                    Form.ChangeYouTubeDomainsCountLabel(count);
                    break;
                case "DiscordList.txt":
                    Form.ChangeDiscordDomainsCountLabel(count);
                    break;
                case "UserServicesList.txt":
                    Form.ChangeUserServicesDomainsCountLabel(count);
                    break;
                default:
                    return;
            }
        }

        private static string GetOneFullStrategy
        {
            get
            {
                StringBuilder strategiesBuilder = new StringBuilder();
                string fullStrategy = string.Empty;
                strategiesBuilder.AppendLine(YouTubeStrategy);
                strategiesBuilder.AppendLine(" --new " + DiscordStrategy);
                if (FilesAndDirectories.GetLinesCount(FilesAndDirectories.HostsPaths["UserServicesHostsPath"]) != 0)
                    strategiesBuilder.AppendLine(" --new " + UserServicesStrategy);
                string dirtyStrategy = strategiesBuilder.ToString();
                List<string> tcpPorts = GetPorts(dirtyStrategy, RegexManager.TcpPortsRegex());
                List<string> udpPorts = GetPorts(dirtyStrategy, RegexManager.UdpPortsRegex());
                string cleanedString = GetCleanedString(dirtyStrategy);
                if (tcpPorts.Count != 0) fullStrategy += $"--wf-tcp={string.Join(',', tcpPorts)} ";
                if (udpPorts.Count != 0) fullStrategy += $"--wf-udp={string.Join(',', udpPorts)} ";
                fullStrategy = RegexManager.WhiteSpaceClean().Replace(fullStrategy + cleanedString, " ");
                return fullStrategy;
            }
        }

        private static string TryCreateWiderRange(List<string> ranges)
        {
            List<(int minRange, int maxRange)> allRanges = new List<(int minRange, int maxRange)>(ranges.Select(r =>
            {
                var rangeSplit = r.Split('-');
                return (minRange: int.Parse(rangeSplit[0]), maxRange: int.Parse(rangeSplit[1]));
            }));
            return $"{allRanges.Min(r => r.minRange)}-{allRanges.Max(r => r.maxRange)}";
        }

        private static List<string> GetPorts(string dirtyStrategy, Regex portRegex)
        {
            List<string> ports = new List<string>();
            foreach (var port in portRegex.Matches(dirtyStrategy).Cast<Match>().Select(m => m.Groups[1].Value).ToList())
            {
                foreach (var splittedPort in port.Split(','))
                {
                    if (splittedPort.Contains('-') && ConfigManager.CurrentConfig.ConcatPortRanges)
                    {
                        var ranges = ports.Where(p => p.Contains('-')).ToList();
                        ports.RemoveAll(p => ranges.Contains(p));
                        ranges.Add(splittedPort);
                        string widerRange = TryCreateWiderRange(ranges);
                        ports.Add(widerRange);
                        continue;
                    }
                    if (!ports.Contains(splittedPort))
                        ports.Add(splittedPort);
                }
            }
            return ports;
        }

        private static string GetCleanedString(string dirtyString)
        {
            string cleanString = RegexManager.TcpPortsRegex().Replace(dirtyString, "");
            cleanString = RegexManager.UdpPortsRegex().Replace(cleanString, "").Replace("\n", "").Replace("\r", "");
            return cleanString;
        }
    }
}
