namespace FactorioLib;

public class FactorioMods(string directory)
{
    public IEnumerable<Mod> ListModFiles()
    {
        if (Directory.Exists(directory))
        {
            var files = Directory.GetFiles(directory);
            var mods = new List<Mod>();

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (!fileName.EndsWith(".zip")) continue;
                // Mod found
                var mod = new Mod() { Path = file };
                var fullName = Path.GetFileNameWithoutExtension(file);
                var strings = fullName.Split('_');
                if (strings.Length != 2)
                {
                    continue;
                }
                
                var fileInfo = new FileInfo(file);
                mod.Name = strings[0];
                mod.Version = strings[1];
                mod.Size = fileInfo.Length;

                mods.Add(mod);
            }

            return mods;
        }

        throw new Exception($"Directory {directory} does not exist");
    }
}

public class Mod
{
    public string Name { get; set; }
    public string Version { get; set; }
    public string Path { get; set; }
    public long Size { get; set; }
    
    public bool Enabled { get; set; }
}