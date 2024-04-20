using System.Text.Json.Serialization;

namespace FactorioLib.Types;

public class Result
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; }

    [JsonPropertyName("owner")] public string Owner { get; set; }

    [JsonPropertyName("summary")] public string Summary { get; set; }

    [JsonPropertyName("downloads_count")] public int DownloadsCount { get; set; }

    [JsonPropertyName("category")] public string Category { get; set; }

    [JsonPropertyName("score")] public double Score { get; set; }

    [JsonPropertyName("latest_release")] public LatestRelease LatestRelease { get; set; }
}