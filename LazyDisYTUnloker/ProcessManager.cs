using LazyDisYTUnlocker.Properties;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace LazyDisYTUnlocker
{
    internal static class ProcessManager
    {
        internal static MainForm Form { get; set; } = null!;

        private static List<Process?> _processList = new List<Process?>();

        private static readonly Dictionary<string, string> _pathsReplace = new Dictionary<string, string>()
        {
            { "[zapret]", FilesAndDirectories.NewZapretDirectory },
            { "[bins]", Path.GetFullPath(FilesAndDirectories.BinarysDirPath) },
            { "[hosts]", Path.GetFullPath(FilesAndDirectories.HostsDirectory) },
            { "[winwsdir]", FilesAndDirectories.NewZapretDirectory }
        };

        private static readonly Regex _replacementRegex = new Regex(string.Join('|', _pathsReplace.Keys.Select(Regex.Escape)));

        internal static async Task<bool> RunStrategies()
        {
            try
            {
                _processList.Add(Process.Start(new ProcessStartInfo() { FileName = FilesAndDirectories.WinwsPath, Arguments = _replacementRegex.Replace(Strategies.DiscordStrategy, match => _pathsReplace[match.Value]), CreateNoWindow = true, UseShellExecute = false }));
                _processList.Add(Process.Start(new ProcessStartInfo() { FileName = FilesAndDirectories.WinwsPath, Arguments = _replacementRegex.Replace(Strategies.YouTubeStrategy, match => _pathsReplace[match.Value]), CreateNoWindow = true, UseShellExecute = false }));
                if (FilesAndDirectories.GetUserServicesDomainsLines.Length > 0)
                    _processList.Add(Process.Start(new ProcessStartInfo() { FileName = FilesAndDirectories.WinwsPath, Arguments = _replacementRegex.Replace(Strategies.UserServicesStrategy, match => _pathsReplace[match.Value]), CreateNoWindow = true, UseShellExecute = false }));
                StartProcessMonitoring().ConfigureAwait(false);
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
            _processList.ForEach(p => p?.Kill());
            _processList.Clear();
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

        private static async Task StartProcessMonitoring()
        {
            while (_processList.Count > 0)
            {
                await Task.Delay(TimeSpan.FromMinutes(2));
                if (_processList.Any(p => p.HasExited))
                {
                    StopStrategies();
                    Form.BeginInvoke(Form.MainButton.PerformClick);
                    MessageBox.Show(StringsLocalization.ZapretProcessesClosed, "Error O_o", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }
        }
    }
}
