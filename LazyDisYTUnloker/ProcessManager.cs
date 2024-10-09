using System.Diagnostics;

namespace LazyDisYTUnloker
{
    internal static class ProcessManager
    {
        private static Process? _discordUnlockProcess { get; set; }
        private static Process? _youtubeUnlockProcess { get; set; } 

        internal static bool RunUnlocks()
        {
            try
            {
                _discordUnlockProcess = Process.Start(new ProcessStartInfo() { FileName = $"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories.WinwsDirectory}\\winws.exe", Arguments = $"--wf-tcp=443-65535 --wf-udp=443-65535 --filter-udp=443 --hostlist=\"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories.WinwsDirectory}\\list-discord.txt\" --dpi-desync=fake --dpi-desync-udplen-increment=10 --dpi-desync-repeats=6 --dpi-desync-udplen-pattern=0xDEADBEEF --dpi-desync-fake-quic=\"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories.WinwsDirectory}\\quic_initial_www_google_com.bin\" --new \r\n--filter-udp=50000-65535 --dpi-desync=fake,tamper --dpi-desync-any-protocol --dpi-desync-fake-quic=\"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories.WinwsDirectory}\\quic_initial_www_google_com.bin\" --new --filter-tcp=443 --hostlist=\"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories.WinwsDirectory}\\list-discord.txt\" --dpi-desync=fake,split2 --dpi-desync-autottl=2 --dpi-desync-fooling=md5sig --dpi-desync-fake-tls=\"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories.WinwsDirectory}\\tls_clienthello_www_google_com.bin\"", CreateNoWindow = true, UseShellExecute = false });
                _youtubeUnlockProcess = Process.Start(new ProcessStartInfo() { FileName = $"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories.WinwsDirectory}\\winws.exe", Arguments = $"--wf-tcp=80,443 --wf-udp=443 --filter-udp=443 --hostlist=\"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories.WinwsDirectory}\\list-youtube.txt\" --dpi-desync=fake --dpi-desync-repeats=11 --dpi-desync-fake-quic=\"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories.WinwsDirectory}\\quic_initial_www_google_com.bin\" --new --filter-udp=443 --dpi-desync=fake --dpi-desync-repeats=11 --new \r\n--filter-tcp=80 --dpi-desync=fake,split2 --dpi-desync-autottl=2 --dpi-desync-fooling=md5sig --new --filter-tcp=443 --hostlist=\"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories.WinwsDirectory}\\list-youtube.txt\" --dpi-desync=fake,split2 --dpi-desync-autottl=2 --dpi-desync-fooling=md5sig --dpi-desync-fake-tls=\"{FilesAndDirectories.MainZapretDirectory}\\{FilesAndDirectories.WinwsDirectory}\\tls_clienthello_www_google_com.bin\" --new --dpi-desync=fake,disorder2 --dpi-desync-autottl=2 --dpi-desync-fooling=md5sig", CreateNoWindow = true, UseShellExecute = false });
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

        internal static void StopUnlocks()
        {
            _discordUnlockProcess?.Kill();
            _youtubeUnlockProcess?.Kill();
        }

    }
}
