using System.Text.Json.Serialization;

public class InfoJson
{
    [JsonPropertyName("factorio_version")] public string FactorioVersion { get; set; }
}