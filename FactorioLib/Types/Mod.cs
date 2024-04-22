using Semver;

namespace FactorioLib.Types;

public class Mod
{
    public string Name { get; set; }
    public SemVersion? LatestVersion { get; set; }
    public SemVersion? LocalVersion { get; set; }
    public bool Enabled { get; set; }

    /// <summary>
    /// True if a mod file is present. False if the mod is present in the mod file but not on disk.
    /// </summary>
    public bool Present { get; set; }

    public IList<ModFile> Files { get; set; } = new List<ModFile>();
}