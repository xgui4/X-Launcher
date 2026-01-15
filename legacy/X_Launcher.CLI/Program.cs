using X_Launcher_Cli.Model;
using X_Launcher_CLI.Views;

namespace X_Launcher_CLI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        ConsoleDisplay consoleDisplay = new();
        try
        {
            await MainView.View(args);
        }
        catch (Exception e)
        {
            consoleDisplay.PrintNewLine("Oops! Something went wrong!", (int)ConsoleColor.Red);
            consoleDisplay.PrintNewLine(e.ToString(), (int)ConsoleColor.Red);
        }
    }
}