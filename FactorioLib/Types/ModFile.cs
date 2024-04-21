namespace FactorioLib;

/// <summary>
/// ModListFile represents a mod file stored on disk in the factorio mods directory.
/// </summary>
public class ModFile
{
    public string Name { get; set; }
    public string Version { get; set; }
    public string Path { get; set; }
    public long Size { get; set; }
}