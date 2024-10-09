using System.Security.Principal;

namespace LazyDisYTUnloker
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
                Application.Run(new MainForm());
            }
            else
                MessageBox.Show("Для работы нужны права администратора!", "Важно, и очень :О", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}