using System.CommandLine;
using FactorioLib;
using Spectre.Console;

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

        var table = new Table();
        table.AddColumn("Name");
        table.AddColumn("Local Version");
        table.AddColumn("Latest Version");
        table.AddColumn("Enabled");
        table.AddColumn("Present");


        foreach (var listModFile in listModFiles)
        {
            string[] rowData = new string[5];
            
            rowData[0] = listModFile.Name;
            if (listModFile.LocalVersion == null)
            {
                rowData[1] = "[maroon]N/A[/]";
            }
            else
            {
                rowData[1] = listModFile.LocalVersion.ToString();
            }
            
            
            if (listModFile.LocalVersion != null && listModFile.LatestVersion != null)
            {
                if (listModFile.LocalVersion.ComparePrecedenceTo(listModFile.LatestVersion) < 0)
                {
                    rowData[2] = $"[green]{listModFile.LatestVersion}[/]";
                }
                else
                {
                    rowData[2] = listModFile.LatestVersion?.ToString() ?? string.Empty;
                }
            }
            else
            {
                rowData[2] = listModFile.LatestVersion?.ToString() ?? string.Empty;
            }
            rowData[3] = listModFile.Enabled.ToString();
            rowData[4] = listModFile.Present.ToString();
            table.AddRow(rowData);
        }

        AnsiConsole.Write(table);
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