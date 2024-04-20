using System.Text.Json.Serialization;

namespace FactorioLib.Types;

public class Links
{
    [JsonPropertyName("first")] public object First { get; set; }

    [JsonPropertyName("next")] public string Next { get; set; }

    [JsonPropertyName("prev")] public object Prev { get; set; }

    [JsonPropertyName("last")] public string Last { get; set; }
}