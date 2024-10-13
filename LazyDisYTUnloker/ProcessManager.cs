using System.Diagnostics;

namespace LazyDisYTUnloker
{
    internal static class ProcessManager
    {
        internal static MainForm Form { get; set; } = null!;

        private static Process? _discordUnlockProcess { get; set; }
        private static Process? _youtubeUnlockProcess { get; set; } 

        internal static bool RunStrategies()
        {
            try
            {
                _discordUnlockProcess = Process.Start(new ProcessStartInfo() { FileName = $"{FilesAndDirectories._mainZapretDirectory}\\{FilesAndDirectories._winwsDirectory}\\winws.exe", Arguments = Strategies.DiscordStrategy.Replace("[winwsdir]", $"{FilesAndDirectories._mainZapretDirectory}\\{FilesAndDirectories._winwsDirectory}"), CreateNoWindow = true, UseShellExecute = false });
                _youtubeUnlockProcess = Process.Start(new ProcessStartInfo() { FileName = $"{FilesAndDirectories._mainZapretDirectory}\\{FilesAndDirectories._winwsDirectory}\\winws.exe", Arguments = Strategies.YouTubeStrategy.Replace("[winwsdir]", $"{FilesAndDirectories._mainZapretDirectory}\\{FilesAndDirectories._winwsDirectory}"), CreateNoWindow = true, UseShellExecute = false });
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось запустить обход!\n\n" +
                    $"" +
                    $"Ошибка: {ex.Message}", "Ох ё!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static void StopStrategies()
        {
            _discordUnlockProcess?.Kill();
            _youtubeUnlockProcess?.Kill();
            if (Form.WindivertServiceCB.Checked)
                KillWinDivertService();
        }

        internal static void KillWinDivertService()
        {
            try
            {
                var killWindivert = Process.Start(new ProcessStartInfo() { FileName = "cmd.exe", Arguments = "sc stop windivert", CreateNoWindow = true, UseShellExecute = false });
                killWindivert.Kill();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось убить службу Windivert.\n\n" +
                $"" +
                $"Ошибка: {ex.Message}", "Пора нанимать нового хитмана...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
