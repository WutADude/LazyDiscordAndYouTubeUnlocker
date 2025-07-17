using LazyDisYTUnlocker.Properties;
using System.Diagnostics;

namespace LazyDisYTUnlocker
{
    internal static class ProcessManager
    {
        internal static MainForm Form { get; set; } = null!;

        private static Process? _winws { get; set; }

        internal static bool RunStrategies()
        {
            try
            {
                string finStrategy = Strategies.FinalStrategy;
                _winws = new Process();
                _winws.StartInfo = new ProcessStartInfo() {
                    FileName = FilesAndDirectories.WinwsPath,
                    Arguments = finStrategy,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                _winws.Exited += P_Exited;
                _winws.EnableRaisingEvents = true;
                _winws.Start();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(StringsLocalization.ProcessManagerStartErrorText.Replace("%error%", ex.Message), StringsLocalization.ProcessManagerStartErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static void P_Exited(object? sender, EventArgs e)
        {
            if (Form.CurrentlyWorking)
                MessageBox.Show(StringsLocalization.ZapretProcessesClosed);
            StopStrategies();
        }

        internal static void StopStrategies()
        {
            try
            {
                _winws?.Kill();
                _winws?.Dispose();
            }
            catch { }
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
