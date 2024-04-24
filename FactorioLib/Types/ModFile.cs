using Semver;

namespace FactorioLib;

/// <summary>
/// ModListFile represents a mod file stored on disk in the factorio mods directory.
/// </summary>
public class ModFile
{
    /// <summary>
    /// The name of the mod.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The version of the mod.
    /// </summary>
    public string Version { get; set; }

    public SemVersion SemVersion { get; set; }
    /// <summary>
    /// The path to the mod file.
    /// </summary>
    public string Path { get; set; }
    public long Size { get; set; }
}