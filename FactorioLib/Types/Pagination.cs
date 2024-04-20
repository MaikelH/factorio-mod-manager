using System.Text.Json.Serialization;

namespace FactorioLib.Types;

public class Pagination
{
    [JsonPropertyName("count")] public int Count { get; set; }

    [JsonPropertyName("page")] public int Page { get; set; }

    [JsonPropertyName("page_count")] public int PageCount { get; set; }

    [JsonPropertyName("page_size")] public int PageSize { get; set; }

    [JsonPropertyName("links")] public Links Links { get; set; }
}