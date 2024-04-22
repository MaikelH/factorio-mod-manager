using System.CommandLine;
using ConsoleTables;
using FactorioLib;

namespace FactorioModUpdater;

class Program
{
    static int Main(string[] args)
    {
        var dirOption = new Option<String>(name: "--dir", description: "Factorio mod directory",
            getDefaultValue: GetFactorioModDir);

        var rootCommand = new RootCommand("Factorio mod utility");
        var modsCommand = new Command("mod", "Manipulate mods");

        var updateModsCommand = new Command("update");
        var listModsCommand = new Command("list") { dirOption };
        modsCommand.AddCommand(updateModsCommand);
        modsCommand.AddCommand(listModsCommand);

        rootCommand.AddCommand(modsCommand);
        updateModsCommand.SetHandler(() => { Console.WriteLine("Mods command executed"); });

        listModsCommand.SetHandler(ListMods, dirOption);

        return rootCommand.InvokeAsync(args).Result;
    }

    private static async Task ListMods(string dir)
    {
        if (dir == "")
        {
            Console.Error.WriteLine("[!] ERROR --dir option or FACTORIO_DIR environment variable must be set");
            return;
        }

        FactorioMods mods = new FactorioMods(dir);
        var listModFiles = await mods.List(true);

        var table = new ConsoleTable("Name", "Local Version", "Latest Version", "Enabled", "Present")
        {
            Options =
            {
                NumberAlignment = Alignment.Right
            }
        };
        foreach (var listModFile in listModFiles)
        {
            table.AddRow(listModFile.Name, listModFile.LocalVersion, listModFile.LatestVersion, listModFile.Enabled, listModFile.Present);
        }

        table.Write(Format.Minimal);
    }

    private static string GetFactorioModDir()
    {
        var modDir = Environment.GetEnvironmentVariable("FACTORIO_MOD_DIR");
        if (modDir != null)
        {
            return modDir;
        }

        return "";
    }
}