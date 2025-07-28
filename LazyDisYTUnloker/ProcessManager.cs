using LazyDisYTUnlocker.Properties;
using System.Diagnostics;
using System.Xml;

namespace LazyDisYTUnlocker
{
    internal static class ProcessManager
    {
        internal static MainForm Form { get; set; } = null!;

        private static Process? _winws { get; set; }

        private static string _autoRunTaskName = "LazyUnlocker";

        internal static bool RunStrategies()
        {
            try
            {
                string finStrategy = Strategies.FinalStrategy;
                _winws = new Process();
                _winws.StartInfo = new ProcessStartInfo()
                {
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

        internal static void CreateAutoRunTask()
        {
            try
            {
                Process.Start(new ProcessStartInfo() { FileName = "cmd.exe", Arguments = $"/C schtasks /create /tn \"{_autoRunTaskName}\" /tr \"\"{Application.ExecutablePath}\" -auto_run\" /sc onlogon /rl highest /f", CreateNoWindow = true, UseShellExecute = false })!.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(StringsLocalization.AutoRunEnableError.Replace("%error%", ex.Message), StringsLocalization.AutoRunErrorMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static void DeleteAutoRunTask()
        {
            try
            {
                Process.Start(new ProcessStartInfo() { FileName = "cmd.exe", Arguments = $"/C schtasks /delete /tn \"{_autoRunTaskName}\" /f", CreateNoWindow = true, UseShellExecute = false })!.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(StringsLocalization.AutoRunDisableError.Replace("%error%", ex.Message), StringsLocalization.AutoRunErrorMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static bool IsAutoRunTaskExists
        {
            get
            {
                try
                {
                    var process = Process.Start(new ProcessStartInfo() { FileName = "cmd.exe", Arguments = $"/C schtasks /query /tn \"{_autoRunTaskName}\" /xml", CreateNoWindow = true, UseShellExecute = false, RedirectStandardOutput = true }); // аргумент /xml для получения документа и проверки пути, который указан в задаче
                    process!.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();
                    if (process.ExitCode != 0 || string.IsNullOrWhiteSpace(output))
                        return false;
                    var xml = new XmlDocument();
                    xml.LoadXml(output);
                    string? command = xml["Task"]?["Actions"]?["Exec"]?["Command"]?.InnerText.Trim('"'); // Получаем путь к файлу указанный в задаче
                    if (string.IsNullOrWhiteSpace(command))
                        return false;
                    if (Application.ExecutablePath != command) // Проверяем наличие файла в случае если задача есть, но файл мог быть перенесён - пересоздаём задачу с новыми данными.
                    {
                        DeleteAutoRunTask();
                        CreateAutoRunTask();
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
