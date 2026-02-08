using X_Launcher_Cli.Model;

namespace X_Launcher_CLI.Views;

public class MainView
{
    private const string AppCrashedMessage = "Oops! Something went wrong and the App had crashed! Here some potential important debug info : ";

    private const string HelpMessage = "Help message is not ready du to a future rework";

    public static async Task View(string[] args)
    {
        var console = new ConsoleDisplay();
        try
        {
            if (args.Length == 0)
            {
                await UserView.View();
            }

            else if (args.Length >= 1)
            {
                if (args[0] == "/?" || args[0] == "-?" || args[0] == "--help" || args[0] == "-h") 
                    console.PrintNewLine(HelpMessage);
                else
                    console.Warn("Script mode currently might not work due to the app backend isn't stable yet!");
                    await ScriptView.View(args);
            }
        }
        catch (Exception e)
        {
            console.Error(AppCrashedMessage);
            console.PrintNewLine(e.ToString(), (int)ConsoleColor.Red);
        }
    }
}