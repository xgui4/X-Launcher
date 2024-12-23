using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using X_Launcher_CLI.ViewModels;
using X_Launcher_CLI.Views;
using X_Launcher_Core.Model;
using X_Launcher_Core.Utility;

namespace X_Launcher_CLI;

public class Program
{
    public static async Task Main(string[] args)
    {
        DataSeeder seeder = new();

        Game game = new Game("1.21.4", AppContext.BaseDirectory + "\\.minecraft", 4000, 2000, null, null);
        Launcher launcherObject = new Launcher(game.Path);
        User user = new User("Dev", null, null);

        List<Object> list = [];
        list = seeder.SeedList(list);

        string userInput = "";

        if (args.Length > 0)
        {
            userInput = args[0];

            ConsoleDisplay.PrintNewLine("You are running in Script Mode!", ConsoleDisplayColor.Magenta);

            if (Enum.TryParse(userInput, true, out MainMenuAction action))
            {
                switch (action)
                {
                    case MainMenuAction.Default:
                        ConsoleDisplay.PrintNewLine("Help coming soon !", ConsoleDisplayColor.Red);
                        break;
                    case MainMenuAction.Play:
                        ConsoleDisplay.Refresh();
                        var launcher = new MinecraftLauncherService(game, launcherObject, user);
                        await launcher.LaunchOfflineAsync();
                        break;
                    case MainMenuAction.Install:
                        ConsoleDisplay.Refresh();
                        ConsoleDisplay.PrintNewLine("Install Option isn't implemented yet!", ConsoleDisplayColor.Red);
                        break;
                    case MainMenuAction.Setting:
                        ConsoleDisplay.Refresh();
                        ConsoleDisplay.PrintNewLine("Setting Option isn't implemented yet!", ConsoleDisplayColor.Red);
                        break;
                    case MainMenuAction.About:
                        ConsoleDisplay.Refresh();
                        ConsoleDisplay.PrintNewLine("About Option isn't implemented yet!", ConsoleDisplayColor.Red);
                        break;
                    case MainMenuAction.License:
                        ConsoleDisplay.Refresh();
                        ConsoleDisplay.PrintNewLine("License Option isn't implemented yet!", ConsoleDisplayColor.Red);
                        break;
                }
            }

            else
            {
                ConsoleDisplay.Refresh();
                ConsoleDisplay.PrintNewLine("Invalid Option, pls try again.", ConsoleDisplayColor.Red);
            }
        }

        else { 
        bool toContinue = true;

            while (toContinue)
            {
                ConsoleDisplay.ChangeBackgroundColor(ConsoleDisplayColor.White);

                MenuView.DisplayHeader();

                ConsoleDisplay.ResetColor();

                MenuView.DisplayMenu(list);

                userInput = Console.ReadLine() ?? "";

                if (Enum.TryParse(userInput, true, out MainMenuAction action))
                {
                    switch (action)
                    {
                        case MainMenuAction.Default:
                            ConsoleDisplay.Refresh();
                            ConsoleDisplay.PrintNewLine("Help coming soon !", ConsoleDisplayColor.Red);
                            break;
                        case MainMenuAction.Play:
                            ConsoleDisplay.Refresh();
                            var launcher = new MinecraftLauncherService(game, launcherObject, user);
                            await launcher.LaunchOfflineAsync();
                            break;
                        case MainMenuAction.Install:
                            ConsoleDisplay.Refresh();
                            ConsoleDisplay.PrintNewLine("Install Option isn't implemented yet!", ConsoleDisplayColor.Red);
                            break;
                        case MainMenuAction.Setting:
                            ConsoleDisplay.Refresh();
                            ConsoleDisplay.PrintNewLine("Setting Option isn't implemented yet!", ConsoleDisplayColor.Red);
                            break;
                        case MainMenuAction.About:
                            ConsoleDisplay.Refresh();
                            ConsoleDisplay.PrintNewLine("About Option isn't implemented yet!", ConsoleDisplayColor.Red);
                            break;
                        case MainMenuAction.License:
                            ConsoleDisplay.Refresh();
                            ConsoleDisplay.PrintNewLine("License Option isn't implemented yet!", ConsoleDisplayColor.Red);
                            break;
                        case MainMenuAction.Quit:
                            toContinue = false;
                            break;
                    }
                }

                else
                {
                    ConsoleDisplay.Refresh();
                    ConsoleDisplay.PrintNewLine("Invalid Option, pls try again.", ConsoleDisplayColor.Red);
                }
            }
        }
    }
}