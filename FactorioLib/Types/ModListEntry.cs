using System.Text.Json.Serialization;

namespace FactorioLib;

public class ModListEntry
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }
}