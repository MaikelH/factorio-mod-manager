using System.Text.Json;
using FactorioLib.Types;
using Semver;

namespace FactorioLib;

/// <summary>
/// The FactorioMods class is responsible for managing and manipulating Factorio mods.
/// It provides functionalities such as listing mod files, reading mod list, and listing mods.
/// </summary>
public class FactorioMods
{
    private readonly string _username;
    private readonly string _token;
    private readonly string _directory;
    private readonly bool _authenticated;

    /// <summary>
    /// Initializes a new instance of the FactorioMods class.
    /// </summary>
    /// <param name="directory">The directory where the Factorio mods are located.</param>
    public FactorioMods(string directory)
    {
        if (!Directory.Exists(directory))
        {
            throw new Exception($"Directory {_directory} does not exist");
        }

        _directory = directory;
    }

    public FactorioMods(string directory, string username, string token) : this(directory)
    {
        _username = username;
        _token = token;
        _authenticated = true;
    }

    /// <summary>
    /// Lists all the mod files in the directory.
    /// </summary>
    /// <returns>An IEnumerable of ModFile objects representing the mod files.</returns>
    public IEnumerable<ModFile> ListModFiles()
    {
        var files = Directory.GetFiles(_directory);
        var mods = new List<ModFile>();

        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            if (!fileName.EndsWith(".zip"))
            {
                continue;
            }

            var mod = new ModFile() { Path = file };
            var fullName = Path.GetFileNameWithoutExtension(file);
            var strings = fullName.Split('_');
            if (strings.Length != 2)
            {
                continue;
            }

            var fileInfo = new FileInfo(file);
            mod.Name = strings[0];
            mod.Version = strings[1];
            mod.SemVersion = SemVersion.Parse(mod.Version, SemVersionStyles.Any);
            mod.Size = fileInfo.Length;

            mods.Add(mod);
        }

        return mods;
    }

    /// <summary>
    /// ReadModList reads the mod-list.json file in the factorio mods directory and returns the content.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<ModListEntry> ReadModList()
    {
        var modfilePath = _directory + Path.DirectorySeparatorChar + "mod-list.json";
        if (!File.Exists(modfilePath))
        {
            throw new Exception($"Mod list file on location {modfilePath} cannot be found.");
        }

        var fileContents = File.ReadAllText(modfilePath);
        ModListFile? parsedFile = JsonSerializer.Deserialize<ModListFile>(fileContents);

        if (parsedFile == null)
        {
            throw new Exception("Content of mod-list.json is null");
        }

        return parsedFile.Mods;
    }

    /// <summary>
    /// Lists all the mods in the directory including version information and shows if they are enabled.
    /// </summary>
    /// <returns>An IEnumerable of Mod objects representing the mods.</returns>
    public async Task<IEnumerable<Mod>> List(bool checkForUpdates = false)
    {
        var modFiles = ListModFiles();
        var modList = ReadModList();

        var modEntries = new Dictionary<string, Mod>();

        foreach (var listEntry in modList)
        {
            modEntries[listEntry.Name] = new Mod()
            {
                Enabled = listEntry.Enabled,
                Name = listEntry.Name
            };
        }

        foreach (var modFile in modFiles)
        {
            if (modEntries.ContainsKey(modFile.Name))
            {
                modEntries[modFile.Name].Files.Add(modFile);
                modEntries[modFile.Name].Present = true;
                if (modFile.SemVersion.ComparePrecedenceTo(modEntries[modFile.Name].LatestVersion) >= 0)
                {
                    modEntries[modFile.Name].LocalVersion = modFile.SemVersion;
                }
            }
            else
            {
                modEntries[modFile.Name] = new Mod()
                {
                    Name = modFile.Name,
                    Files = new List<ModFile> { modFile },
                    Enabled = false,
                    LocalVersion = modFile.SemVersion
                };
            }
        }

        if (!checkForUpdates)
        {
            return modEntries.Values;
        }
        
        // Check for mod updates
        ModPortal modPortal = new();
        ModsRequestParameters parameters = new()
        {
            NameList = modEntries.Keys.ToArray(),
            ReturnAll = true
        };

        var mods = await modPortal.GetMods(parameters);
        foreach (var mod in mods.Results)
        {
            if (modEntries.ContainsKey(mod.Name))
            {
                modEntries[mod.Name].LatestVersion = mod.Releases.Last().Version;
            }
        }

        return modEntries.Values;
    }

    public async Task UpdateMod(string modName, SemVersion? modLatestVersion)
    {
        throw new NotImplementedException();
    }
}