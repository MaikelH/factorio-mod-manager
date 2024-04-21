namespace FactorioLib.Types;

public class Mod
{
    public string Name { get; set; }
    public string LatestVersion { get; set; }
    public bool Enabled { get; set; }
    public IList<ModFile> Files { get; set; } = new List<ModFile>();
}