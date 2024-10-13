using System.Text.Json;

namespace LazyDisYTUnloker
{
    internal static class ConfigManager
    {
        private const string _configName = "appconfig.json";

        private static bool _configExists => File.Exists(_configName);

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
    }
}
