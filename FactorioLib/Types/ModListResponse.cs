using System.Text.Json.Serialization;

namespace FactorioLib.Types;

public class ModListResponse
{
    [JsonPropertyName("pagination")] public Pagination Pagination { get; set; }

    [JsonPropertyName("results")] public List<Result> Results { get; set; } = new();
}