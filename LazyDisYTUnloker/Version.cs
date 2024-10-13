using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace LazyDisYTUnloker
{
    internal static class Version
    {
        internal static MainForm Form { get; set; } = null!;

        internal static string CurrentAppVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion??"empty version";
        internal static string LatestAppVersion { get; private set; } = null!;

        internal static async Task<bool> IsNewVersionAvailable()
        {
            try
            {
                Form.ChangeStatus("проверяю версию лаунчера...");
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-agent", $"LazyDiscordAndYouTubeUnlocker(V{CurrentAppVersion})");
                    using (var jsonDocument = JsonDocument.Parse(await client.GetStringAsync(DataURLs.VersionInfoURL)))
                    {
                        var rootElement = jsonDocument.RootElement;
                        LatestAppVersion = (rootElement.GetProperty("tag_name").GetString()??CurrentAppVersion).Replace("V", "");
                        if (CurrentAppVersion != LatestAppVersion)
                            return true;
                        return false;
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
