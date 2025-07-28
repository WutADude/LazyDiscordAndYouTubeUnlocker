using LazyDisYTUnlocker.Properties;
using System.Security.Principal;

namespace LazyDisYTUnlocker
{
    internal static class Program
    {
        private static Mutex? _appMutex { get; set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            string uniqueAppName = $"LazyDisYTUnlocker_5d38b8ea-bddd-4b1e-8678-8839c0319aad";
            try
            {
                _appMutex = new Mutex(true, uniqueAppName, out bool createdNew);
                if (!createdNew)
                {
                    MessageBox.Show(StringsLocalization.AppAlreadyExistsMessage, StringsLocalization.AppAlreadyExistsMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
                {
                    ApplicationConfiguration.Initialize();
                    ConfigManager.SetupConfig();
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(ConfigManager.CurrentConfig.ChoosenCulture);
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(ConfigManager.CurrentConfig.ChoosenCulture);
                    Application.Run(new MainForm(args));
                }
                else
                    MessageBox.Show(StringsLocalization.AdminRightsMessageText, StringsLocalization.AdminRightsMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(StringsLocalization.UnhandledErrorMessage.Replace("%error%", ex.Message), StringsLocalization.UnhandledErrorMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                _appMutex?.ReleaseMutex();
                _appMutex?.Dispose();
            }
        }
    }
}