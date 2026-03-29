using X_Launcher_Cli.Model;
using X_Launcher_CLI.ViewModels;
using X_Launcher_Core.Model;
using X_Launcher_Core.Service;

namespace X_Launcher_CLI.Views;

public class UserView
{
    public static async Task View()
    {
        ConsoleDisplay console = new();
        var toContinue = true;
        ConfigService configSavingService = new ConfigService(console);


        DataSeeder seeder = new();

        var game = new GameConfig("1.21.4", AppContext.BaseDirectory + "/.minecraft", 4000, 2000);
        var launcherObject = new LauncherConfig(game.Path ?? GameConfig.DefaultVersion);
        var user = new SessionInfo("Dev");

        List<object> list = [];
        list = seeder.SeedList(list);

        var userInput = "";

        while (toContinue)
        {
            MenuView.DisplayHeader();

            console.ResetColor();

            MenuView.DisplayMenu(list);

            userInput = Console.ReadLine() ?? "";

            if (Enum.TryParse(userInput, true, out Features action))
            {
                switch (action)
                {
                    case Features.Play:
                        console.Refresh();
                        var launcher = new MinecraftLauncherService(game, launcherObject, user, console);
                        await launcher.LaunchOnlineSessionAsync();
                        break;
                    case Features.Install:
                        console.Refresh();
                        console.PrintNewLine("Install Option isn't implemented yet!", (int)ConsoleColor.Red);
                        break;
                    case Features.SaveConfig:
                        console.Refresh();
                        console.PrintNewLine("Choose a version : ");
                        string version = Console.ReadLine() ?? GameConfig.DefaultVersion;
                        console.PrintNewLine("Choose a path : ");
                        string? path = Console.ReadLine();
                        console.PrintNewLine("Choose a username : "); 
                        string username = Console.ReadLine() ?? "";
                        console.PrintNewLine($"{username}, choose the name which the save be named (the .xprofile is already automatically added) : ");
                        string nameOfFile = Console.ReadLine() ?? "save.xprofile";
                        configSavingService.SaveConfig(new ConfigFile(new GameConfig(version, path), new LauncherConfig(), new SessionInfo(username)), nameOfFile + ".xprofile");
                        break;
                    case Features.Login:
                        console.Refresh();
                        console.Info("Login View is not available yet!");
                        break;
                    case Features.Setting:
                        console.Refresh();
                        console.PrintNewLine("Setting Option isn't implemented yet!", (int)ConsoleColor.Red);
                        break;
                    case Features.Quit:
                        toContinue = false;
                        break;
                }
            }

            else
            {
                console.Refresh();
                console.PrintNewLine("Invalid Option, pls try again.", (int)ConsoleColor.Red);
            }
        }
    }
}