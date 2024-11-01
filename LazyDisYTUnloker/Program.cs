using LazyDisYTUnlocker.Properties;
using System.Security.Principal;

namespace LazyDisYTUnlocker
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                ApplicationConfiguration.Initialize();
                ConfigManager.SetupConfig();
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(ConfigManager.CurrentConfig.ChoosenCulture);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(ConfigManager.CurrentConfig.ChoosenCulture);
                Application.Run(new MainForm());
            }
            else
                MessageBox.Show(StringsLocalization.AdminRightsMessageText, StringsLocalization.AdminRightsMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}