using X_Launcher_Cli.Model;
using X_Launcher_CLI.ViewModels;
using X_Launcher_Core.Model;
using X_Launcher_Core.Service;

namespace X_Launcher_CLI.Views;

public class ScriptView
{
    public static async Task View(string[] args)
    {
        var console = new ConsoleDisplay();
        var userInput = args[0];
        var game = new GameConfig();
        var launcherObject = new LauncherConfig();
        var user = new SessionInfo();
        var versionArgument = GameConfig.DefaultVersion; 
        var pathArgument = String.Empty;
        var usernameArgument= String.Empty;
        var emailArgument = String.Empty;
        var passwordArgument = String.Empty;
        
        if (args.Length > 1)
        {
            versionArgument = args[1];
            if (args.Length > 2)
            {
                pathArgument = args[2];
            }

            if (args.Length > 3)
            {
                usernameArgument = args[3];
            }

            if (args.Length > 4)
            {
                emailArgument = args[4];
            }

            if (args.Length > 5)
            {
                passwordArgument = args[5];
            }
            
            game.FillGameConfig(versionArgument, pathArgument, null, null, GameConfig.DefaultMaximumMemory, GameConfig.DefaultMinimumMemory);
            
            user.FillUserInfo(usernameArgument, emailArgument, passwordArgument);

            launcherObject.FillLauncherConfig(game.Path ?? Environment.CurrentDirectory + "/.minecraft");
        }

        console.PrintNewLine("You are running in Script Mode!", (int)ConsoleColor.Magenta);

        if (Enum.TryParse(userInput, true, out Features action))
        {
            switch (action)
            {
                default:
                    console.PrintNewLine("Invalid action!", (int)ConsoleColor.Magenta);
                    break;
                case Features.Play:
                    console.Refresh();
                    try
                    {
                        var launcher = new MinecraftLauncherService(game, launcherObject, user, console);
                        await launcher.LaunchOnlineSessionAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    break;
                case Features.Install:
                    console.Refresh();
                    console.Info("Install Option isn't implemented yet!");
                    break;
                case Features.SaveConfig:
                    console.Refresh();
                    console.Info("Saving ConfigFile in Script Mode isn't supported yet!");
                    break;
                case Features.Setting:
                    console.Refresh();
                    console.Info("Setting Option isn't implemented yet!");
                    break;
                case Features.Login:
                    console.Refresh();
                    console.Info("Not Supported in Script Mode!");
                    break; 
                case Features.Quit:
                    console.Warn("Quit Option isn't usable in Script Mode!");
                    break;
            }
        }

        else
        {
            console.Refresh();
            console.Error("Invalid Option, pls try again.");
        }
    }
}