using System.Text.RegularExpressions;

namespace LazyDisYTUnlocker
{
    public static partial class RegexManager
    {
        [GeneratedRegex("\\-\\-wf\\-tcp=([\\d\\-\\,]+)", RegexOptions.IgnoreCase)]
        public static partial Regex TcpPortsRegex();

        [GeneratedRegex("\\-\\-wf\\-udp=([\\d\\-\\,]+)", RegexOptions.IgnoreCase)]
        public static partial Regex UdpPortsRegex();

        [GeneratedRegex(@"\s+", RegexOptions.IgnoreCase)]
        public static partial Regex WhiteSpaceClean();

        public static readonly Regex ReplacementRegex = new Regex(string.Join('|', FilesAndDirectories.PathsReplace.Keys.Select(Regex.Escape)));
    }
}
