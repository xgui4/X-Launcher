# X Launcher Core

![Logo](assets/app-icon.ico)

A  Cross-Platform FOSS Minecraft®️ Launcher written in C# for the backend and Pyside6 (Python/QT) for the frontend

This project follow the MVVM patten.

## Framework/Library used

- C# Backend
  - .NET 9
  - CmlLib
  - Community Toolkit MVVM

- Legacy Frontend
  - Avalonia®️ UI (for the legacy UI)
  - .NET 9
  - C#

- QT Frontend
  - Python 3
  - Pyside6 (QT)

## Project Structure

- `assets/`
  - Global Assets
- `backend/`
  - `X_Launcher.Core.Service/`
    - New Experimental Backend
- `src/`
  - New Experimental Frontend written in Python with Pyside6 (QT)
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
- `locales/`
  - localisations files for mutli-language supports
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

## Others Informationss

License : MIT

Code of conduct :  [code-of-conduct.md](code-of-conduct.md)

## Legal Disclaimer

This is NOT AN OFFICIAL MINECRAFT [PRODUCT/SERVICE/EVENT/etc.]. NOT APPROVED BY OR ASSOCIATED WITH MOJANG OR MICROSOFT.
