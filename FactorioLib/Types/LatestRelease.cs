using System.Text.Json.Serialization;

namespace FactorioLib.Types;

public class LatestRelease
{
    [JsonPropertyName("download_url")] public string DownloadUrl { get; set; }

    [JsonPropertyName("file_name")] public string FileName { get; set; }

    [JsonPropertyName("info_json")] public InfoJson InfoJson { get; set; }

    [JsonPropertyName("released_at")] public DateTime ReleasedAt { get; set; }

    [JsonPropertyName("version")] public string Version { get; set; }

    [JsonPropertyName("sha1")] public string Sha1 { get; set; }
}