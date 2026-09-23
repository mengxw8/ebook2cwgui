using Newtonsoft.Json;
using System.Reflection;
using System.Xml.Linq;

namespace CW
{
    internal sealed class ReleaseInfo
    {
        public required Version Version { get; init; }
        public required string VersionText { get; init; }
        public required string Url { get; init; }
        public string? Notes { get; init; }
    }

    internal static class UpdateChecker
    {
        private const string ReleasesFeedUrl = "https://github.com/mengxw8/ebook2cwgui/releases.atom";
        private static readonly TimeSpan CacheLifetime = TimeSpan.FromHours(6);
        private static readonly XNamespace Atom = "http://www.w3.org/2005/Atom";

        public static Version CurrentVersion =>
            Assembly.GetExecutingAssembly().GetName().Version ?? new Version(1, 0, 0, 0);

        public static async Task<ReleaseInfo?> FindNewerReleaseAsync(CancellationToken cancellationToken = default)
        {
            var cached = ReadCache();
            if (cached != null && DateTimeOffset.UtcNow - cached.CheckedAt < CacheLifetime)
                return NewerThanCurrent(cached);

            try
            {
                var latest = await FetchLatestAsync(cancellationToken);
                WriteCache(latest);
                return NewerThanCurrent(latest);
            }
            catch (Exception)
            {
                return NewerThanCurrent(cached);
            }
        }

        private static async Task<ReleaseInfo?> FetchLatestAsync(CancellationToken cancellationToken)
        {
            using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(8) };
            client.DefaultRequestHeaders.UserAgent.ParseAdd("ebook2cwgui");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/atom+xml");

            // 发布页的 Atom 源不走 api.github.com，避免未登录接口每小时 60 次的限制。
            var xml = await client.GetStringAsync(ReleasesFeedUrl, cancellationToken);
            var entry = XDocument.Parse(xml).Root?.Element(Atom + "entry");
            if (entry == null)
                return null;

            var title = entry.Element(Atom + "title")?.Value?.Trim();
            if (string.IsNullOrWhiteSpace(title) || !TryParseVersion(title, out var latest))
                return null;

            var url = entry.Elements(Atom + "link")
                .FirstOrDefault(link => (string?)link.Attribute("rel") == "alternate")
                ?.Attribute("href")?.Value;
            if (string.IsNullOrWhiteSpace(url))
                url = "https://github.com/mengxw8/ebook2cwgui/releases";

            return new ReleaseInfo
            {
                Version = latest,
                VersionText = title.StartsWith("v", StringComparison.OrdinalIgnoreCase) ? title : "v" + title,
                Url = url,
            };
        }

        private static ReleaseInfo? NewerThanCurrent(ReleaseInfo? release) =>
            release != null && release.Version > CurrentVersion ? release : null;

        private static ReleaseInfo? NewerThanCurrent(UpdateCheckCache? cache)
        {
            if (cache == null || string.IsNullOrWhiteSpace(cache.VersionText) || string.IsNullOrWhiteSpace(cache.Url))
                return null;
            if (!TryParseVersion(cache.VersionText, out var version) || version <= CurrentVersion)
                return null;
            return new ReleaseInfo
            {
                Version = version,
                VersionText = cache.VersionText,
                Url = cache.Url,
            };
        }

        private static bool TryParseVersion(string tag, out Version version)
        {
            var text = tag.Trim();
            if (text.StartsWith("v", StringComparison.OrdinalIgnoreCase))
                text = text[1..];
            return Version.TryParse(text, out version!);
        }

        private static string CachePath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ebook2cwgui",
            "update-check.json");

        private static UpdateCheckCache? ReadCache()
        {
            try
            {
                if (!File.Exists(CachePath))
                    return null;
                return JsonConvert.DeserializeObject<UpdateCheckCache>(File.ReadAllText(CachePath));
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static void WriteCache(ReleaseInfo? latest)
        {
            try
            {
                var directory = Path.GetDirectoryName(CachePath);
                if (!string.IsNullOrEmpty(directory))
                    Directory.CreateDirectory(directory);
                var cache = new UpdateCheckCache
                {
                    CheckedAt = DateTimeOffset.UtcNow,
                    VersionText = latest?.VersionText,
                    Url = latest?.Url,
                };
                File.WriteAllText(CachePath, JsonConvert.SerializeObject(cache));
            }
            catch (Exception)
            {
                // 缓存写失败不影响本次检查结果。
            }
        }

        private sealed class UpdateCheckCache
        {
            public DateTimeOffset CheckedAt { get; set; }
            public string? VersionText { get; set; }
            public string? Url { get; set; }
        }
    }
}
