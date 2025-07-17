using LazyDisYTUnlocker.Properties;
using System.Diagnostics;
using System.Text.Json;

namespace LazyDisYTUnlocker
{
    internal static class Version
    {
        internal static MainForm Form { get; set; } = null!;

        internal static string CurrentAppVersion { get => FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion ?? "empty version"; }
        internal static string LatestAppVersion { get; private set; } = null!;

        internal static async Task<bool> IsNewVersionAvailable()
        {
            try
            {
                Form.ChangeStatus(StringsLocalization.MainStatusSoftwareVersionCheck);
                using (HttpClient client = new HttpClient() {Timeout = TimeSpan.FromSeconds(3) })
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-agent", $"LazyDiscordAndYouTubeUnlocker(V{CurrentAppVersion})");
                    using (var jsonDocument = JsonDocument.Parse(await client.GetStringAsync(DataURLs.VersionInfoURL)))
                    {
                        var rootElement = jsonDocument.RootElement;
                        LatestAppVersion = (rootElement.GetProperty("tag_name").GetString()??CurrentAppVersion).Replace("V", "");
                        return CurrentAppVersion != LatestAppVersion;
                    }
                }
            }
            catch 
            {
                LatestAppVersion = CurrentAppVersion;
                return false; 
            }
            finally
            {
                Form.ChangeSoftwareVersionLabel(CurrentAppVersion, LatestAppVersion);
            }
        }
    }
}
