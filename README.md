# Factorio Mod Manager
Tool to manage mods for Factorio dedicated servers. It allows you to list, download, update and disable mods.

## Install
There are 2 differennt ways to install: build from source and download the binary release.

### Build from source

1. Clone the git repository
2. Change the directory containing the solution file `FactorioModUpdater.sln`
3. `dotnet restore` to install the dependencies
4. `dotnet build` to build the application

### Install binary
1. On the github page select the newest release.
2. Download the release for your platform (Linux or Windows)

## Commands
The tool must know in which directory the mods are stored. This directory can be supplied in two ways the `--dir` argument or the `FACTORIO_DIR` environment variable

### Root
```
Description:
  Factorio mod manager

Usage:
  FactorioModUpdater [command] [options]

Options:
  --dir <dir>     Factorio mod directory []
  --version       Show version information
  -?, -h, --help  Show help and usage information

Commands:
  mod  Manipulate mods
```

### mod
```
Description:
  Manipulate mods

Usage:
  FactorioModUpdater mod [command] [options]

Options:
  --dir <dir>     Factorio mod directory []
  -?, -h, --help  Show help and usage information

Commands:
  update
  list
```

### mod list
```
Description:
  List all mods present in the Factorio mod directory

Usage:
  FactorioModUpdater mod list [options]

Options:
  --dir <dir>     Factorio mod directory []
  -?, -h, --help  Show help and usage information
```

### mod update
```
Description:
    Update mods in the Factorio mod directory

Usage:
  FactorioModUpdater mod update [options]

Options:
  --user <user>    Username for Factorio mod portal []
  --token <token>  Token for Factorio mod portal []
  --dry-run        Token for Factorio mod portal
  --dir <dir>      Factorio mod directory []
  -?, -h, --help   Show help and usage information
```