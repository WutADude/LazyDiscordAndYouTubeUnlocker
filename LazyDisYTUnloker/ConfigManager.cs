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

        internal static void ResetChoosenStrategies()
        {
            CurrentConfig.ChoosenYouTubeStrategy = CurrentConfig.ChoosenDiscordStrategy = CurrentConfig.ChoosenUserServicesStrategy = 0;
            SaveConfig();
        }

        internal static void SaveConfig() => File.WriteAllText(_configName, JsonSerializer.Serialize(CurrentConfig));
    }

    public class Config
    {
        public int ChoosenYouTubeStrategy { get; set; } // Выбранная стратегия для YouTube (индекс)

        public int ChoosenDiscordStrategy { get; set; } // Выбранная стратегия для Discord (индекс)

        public int ChoosenUserServicesStrategy { get; set; } // Выбранная стратегия для пользовательсих сервисов (индекс)

        public bool HideInTrayOnMinimize { get; set; } = false; // Прячемся в Трей, или нет.

        public bool KillWindivertOnStop { get; set; } = true; // Убиваем Windivert сервис после нажатия кнопки остановки, или нет.

        public string StrategiesSplitString { get; set; } = ">NEW_STRATEGY"; // Разделитель стратегий

        public string ChoosenCulture { get; set; } = "Ru"; // Локализация

        public bool ConcatPortRanges { get; set; } = true; // Разрешение на объединение диапазонов портов в один диапазон. (Например, было 443,500-900,1200-4900 | стало 443,500-4900)
    }
}
