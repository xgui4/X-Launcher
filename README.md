# X Launcher Core

![Logo](assets/app-icon.ico)

A  Cross-Platform FOSS Minecraft®️ Launcher written in C# for the backend and Pyside6 (Python/QT) for the frontend

This project follow the MVVM patten.

## Framework, Library and tools used

- ASP.NET Core backend
  - C# (.NET 9)
  - CmlLib (interface implemented in X_Launcher.Core)

- Avalonia UI (legacy frontend)
  - C# (.NET 9)
  - Community Toolkit MVVM
  - May not receive future update and may be removed in the offical release

- Legacy .NET Console CLI
  - C# (.NET 9)
  - Windows version with a easier sign-in feature 
  - May not receive future update

- QT Frontend
  - Python 3.10+
  - Pyside6
  - JSON Locale
    - easily extensible by creating json file with "key" and "value"

- Automation toos
  - UV (optional)
    - only work on Windows or Linux or MacOS since QT does not release QT binary for BSD in PyPi
  - Taskfile
    - create automation task for packaging and developpement (wip)

- IDE
  - VSCode
  - Jetbrain Rider
  - QT Creator

## Project Structure

- `assets`
  - assets for the pyside 6 frontend
- `locales`
    - locales files for pyside 6 frontend
- `src/`
  - `backend/`
    - `X_Launcher.Service/`
      - New Experimental Backend Server Service
    - `X_Launcher.Core`
      - Experimental backend logic
  - `legacy/`
    - Old Avalonia and C# Console App
    - `X-Launcher.Core/`
      - Dotnet library (an interface) containing the commune classes and method with login logic.
    - `X-Launcher.App/`
      - The code for the GUI / Desktop App
    - `X-Launcher.Desktop/`
      - Boilerplate and runnable code for the Desktop app
    - `X-Launcher.CLI/`
      - The code for the command line interface (CLI)
    - `X-Launcher.CLI.Windows/`
      - Boilerplate and runnable code for enabling Windows specific code or function for the CLI
  - `frontend/`
     - New Experimental Frontend written in Python with Pyside6 (QT)
- `pkg/`
  - the packages formats for the mutltiples OS and Platform
  - `windows/`
    - Files related to packaging for Microsoft Windows
  - `linux/`
    - Files related to packaging for Linux Distributions
  - `freebsd/`
    - Files related to packaging/porting to FreeBSD
- project root
  - docs, config and projects file for both the backend and frontend

## Platform supported

- Windows (only tested the old frontend)
- GNU/Linux (Tested on EndeavourOS (based on Arch BTW))
- *BSD (Only FreeBSD/GhostBSD tested so far)
- MacOS (technically would work but I offer no support for it since I do not own a Mac)

## Running from Source Code
  - Run `./start-x-launcher.py` to lauch the frontend and backend with a logging service 
  - Run `./start-legacy.py` to get a CLI menu to launch old legacy version

## Packaging 
  - Guide will come in the first stable release

## Contributions 
  - Guide will come in the first stable release

## Support/Moderation
  - Code of conduct : [code-of-conduct.md](code-of-conduct.md)
  - Support will come in the first stable release

## License

License : MIT

## Legal Disclaimer

This is NOT AN OFFICIAL MINECRAFT [PRODUCT/SERVICE/EVENT/etc.]. NOT APPROVED BY OR ASSOCIATED WITH MOJANG OR MICROSOFT.
