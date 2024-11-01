using System.Text.Json;

namespace LazyDisYTUnlocker
{
    internal static class ConfigManager
    {
        private const string _configName = "appconfig.json";

        private static bool _configExists { get => File.Exists(_configName); }

        internal static Config CurrentConfig { get; private set; } = null!;
        
        internal static void SetupConfig()
        {
            if (!_configExists)
            {
                CurrentConfig = new Config();
                File.WriteAllText(_configName, JsonSerializer.Serialize(CurrentConfig));
            }
            else
            {
                CurrentConfig = JsonSerializer.Deserialize<Config>(File.ReadAllText(_configName)) ?? new Config();
            }
        }

        internal static void SaveConfig()
        {
            File.WriteAllText(_configName,JsonSerializer.Serialize(CurrentConfig));
        }
    }

    public class Config
    {
        public int ChoosenYouTubeStrategy { get; set; }

        public int ChoosenDiscordStrategy { get; set; }

        public bool HideInTrayOnMinimize { get; set; } = false;

        public bool KillWindivertOnStop { get; set; } = true;

        public string ChoosenCulture { get; set; } = "En";
    }
}
