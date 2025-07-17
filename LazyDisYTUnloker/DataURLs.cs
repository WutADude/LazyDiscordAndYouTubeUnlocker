namespace LazyDisYTUnlocker
{
    internal static class DataURLs
    {
        internal const string VersionInfoURL = "https://api.github.com/repos/WutADude/LazyDiscordAndYouTubeUnlocker/releases/latest";

        internal const string ZapretBundleURL = "https://github.com/bol-van/zapret-win-bundle/archive/refs/heads/master.zip";

        private const string DiscordHostsURL = "https://raw.githubusercontent.com/WutADude/LazyDiscordAndYouTubeUnlocker/refs/heads/master/Hosts/DiscordHosts.txt";

        private const string YouTubeHostsURL = "https://raw.githubusercontent.com/WutADude/LazyDiscordAndYouTubeUnlocker/refs/heads/master/Hosts/YouTubeHosts.txt";

        private const string DiscordStrategiesURL = "https://raw.githubusercontent.com/WutADude/LazyDiscordAndYouTubeUnlocker/refs/heads/master/Strategies/Dscord%20Zapret%20strategy.txt";

        private const string YouTubeStrategiesURL = "https://raw.githubusercontent.com/WutADude/LazyDiscordAndYouTubeUnlocker/refs/heads/master/Strategies/YouTube%20Zapret%20strategy.txt";

        private const string UserServicesStrategiesURL = "https://raw.githubusercontent.com/WutADude/LazyDiscordAndYouTubeUnlocker/refs/heads/master/Strategies/UserServices%20Zapret%20strategy.txt";

        internal static Dictionary<string, string> HostsAndUrls = new Dictionary<string, string>()
        {
            { FilesAndDirectories.HostsPaths["YouTubeHostsPath"], YouTubeHostsURL },
            { FilesAndDirectories.HostsPaths["DiscordHostsPath"], DiscordHostsURL }
        };

        internal static Dictionary<string, string> StrategiesAndUrls = new Dictionary<string, string>()
        {
            { FilesAndDirectories.StrategiesPaths["YouTubeStrategiesPath"], YouTubeStrategiesURL },
            { FilesAndDirectories.StrategiesPaths["DiscordStrategiesPath"], DiscordStrategiesURL },
            { FilesAndDirectories.StrategiesPaths["UserServicesStrategiesPath"], UserServicesStrategiesURL }
        };
    }
}
