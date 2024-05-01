using System.CommandLine;
using FactorioLib;
using FactorioLib.Types;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace FactorioModUpdater;

class Program
{
    static int Main(string[] args)
    {
        var dirOption = new Option<String>(name: "--dir", description: "Factorio mod directory",
            getDefaultValue: GetFactorioModDir);
        var userNameOption = new Option<String>(name: "--user", description: "Username for Factorio mod portal",
            getDefaultValue: GetUsername);
        var tokenOption = new Option<String>(name: "--token", description: "Token for Factorio mod portal",
            getDefaultValue: GetToken);
        var dryRunOption = new Option<Boolean>(name: "--dry-run", description: "Token for Factorio mod portal");

        var rootCommand = new RootCommand("Factorio mod utility");
        rootCommand.AddGlobalOption(dirOption);

        var modsCommand = new Command("mod", "Manipulate mods");

        var updateModsCommand = new Command("update");
        updateModsCommand.AddOption(userNameOption);
        updateModsCommand.AddOption(tokenOption);
        updateModsCommand.AddOption(dryRunOption);

        var listModsCommand = new Command("list");
        modsCommand.AddCommand(updateModsCommand);
        modsCommand.AddCommand(listModsCommand);

        rootCommand.AddCommand(modsCommand);
        listModsCommand.SetHandler(ListMods, dirOption);
        updateModsCommand.SetHandler(UpdateMods, dirOption, userNameOption, tokenOption, dryRunOption);

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

    private static async Task UpdateMods(string dir, string username, string token, Boolean dryRun)
    {
        if (dir == "")
        {
            AnsiConsole.MarkupLine("[red]ERROR --dir option or FACTORIO_DIR environment variable must be set[/]");
            return;
        }

        if (!dryRun)
        {
            if (username == "")
            {
                AnsiConsole.MarkupLine("[red]ERROR --user option or FACTORIO_USER environment variable must be set[/]");
                return;
            }

            if (token == "")
            {
                AnsiConsole.MarkupLine("[red]ERROR --token option or FACTORIO_TOKEN environment variable must be set[/]");
                return;
            }
        }

        FactorioMods factorioMods = new FactorioMods(dir, username, token);
        var mods = await factorioMods.List(true);

        var modsToUpdate = mods.Where(x => x.LocalVersion != null && x.LatestVersion != null)
            .Where(x => x.LocalVersion!.ComparePrecedenceTo(x.LatestVersion) < 0)
            .ToArray();

        AnsiConsole.MarkupLine($"[green]Updating {modsToUpdate.Length} mod(s)[/]");

        foreach (var mod in modsToUpdate)
        {
            AnsiConsole.MarkupLine($"[green]Updating {mod.Name} from {mod.LocalVersion} to {mod.LatestVersion}[/]");

            if (!dryRun)
            {
                await factorioMods.UpdateMod(mod.Name, null);
            }
        }

        if (dryRun)
        {
            AnsiConsole.MarkupLine("[darkorange]Dry-Run enabled, mods not updated[/]");
            return;
        } 
        AnsiConsole.MarkupLine("[green]All mods updated[/]");
    }

    private static string GetFactorioModDir()
    {
        var modDir = Environment.GetEnvironmentVariable("FACTORIO_DIR");
        if (modDir != null)
        {
            return modDir;
        }

        return "";
    }

    private static string GetUsername()
    {
        var username = Environment.GetEnvironmentVariable("FACTORIO_USER");
        if (username != null)
        {
            return username;
        }

        return "";
    }

    private static string GetToken()
    {
        var token = Environment.GetEnvironmentVariable("FACTORIO_TOKEN");
        if (token != null)
        {
            return token;
        }

        return "";
    }
}