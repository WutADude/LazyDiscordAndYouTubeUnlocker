using LazyDisYTUnlocker.Properties;
using System.Diagnostics;

namespace LazyDisYTUnlocker
{
    internal static class ProcessManager
    {
        internal static MainForm Form { get; set; } = null!;

        private static Process? _discordUnlockProcess { get; set; }
        private static Process? _youtubeUnlockProcess { get; set; }
        private static Process? _userServicesUnlockProcess { get; set; }

        private static string _winwsExecutablePath => $"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories._winwsDirectory}\\winws.exe";

        internal static bool RunStrategies()
        {
            try
            {
                _discordUnlockProcess = Process.Start(new ProcessStartInfo() { FileName = _winwsExecutablePath, Arguments = Strategies.DiscordStrategy.Replace("[winwsdir]", $"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories._winwsDirectory}"), CreateNoWindow = true, UseShellExecute = false });
                _youtubeUnlockProcess = Process.Start(new ProcessStartInfo() { FileName = _winwsExecutablePath, Arguments = Strategies.YouTubeStrategy.Replace("[winwsdir]", $"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories._winwsDirectory}"), CreateNoWindow = true, UseShellExecute = false });
                if (FilesAndDirectories.GetUserServicesDomainsLines.Length > 0)
                    _userServicesUnlockProcess = Process.Start(new ProcessStartInfo() { FileName = _winwsExecutablePath, Arguments = Strategies.UserServicesStrategy.Replace("[winwsdir]", $"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories._winwsDirectory}"), CreateNoWindow = true, UseShellExecute = false });
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(StringsLocalization.ProcessManagerStartErrorText.Replace("%error%", ex.Message), StringsLocalization.ProcessManagerStartErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static void StopStrategies()
        {
            _discordUnlockProcess?.Kill();
            _youtubeUnlockProcess?.Kill();
            if (_userServicesUnlockProcess is not null || Process.GetProcesses().Contains(_userServicesUnlockProcess))
                _userServicesUnlockProcess?.Kill();
            if (Form.WindivertServiceCB.Checked)
                KillWinDivertService();
        }

        internal static void KillWinDivertService()
        {
            try
            {
                Process.Start(new ProcessStartInfo() { FileName = "cmd.exe", Arguments = "/C sc stop windivert", CreateNoWindow = true, UseShellExecute = false });
            }
            catch (Exception ex)
            {
                MessageBox.Show(StringsLocalization.WindivertKillErrorText.Replace("%error%", ex.Message), StringsLocalization.WindivertKillErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
