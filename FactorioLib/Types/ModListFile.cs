using System.Text.Json.Serialization;

namespace FactorioLib.Types;

/// <summary>
/// ModListFile represents the mod-list.json file.
/// </summary>
public class ModListFile
{
    [JsonPropertyName("mods")] public IEnumerable<ModListEntry> Mods { get; set; }
}