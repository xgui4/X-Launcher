using X_Launcher_Cli.Model;
using X_Launcher_Core;

namespace X_Launcher_CLI.Views;

public class MenuView
{
    private static readonly ConsoleDisplay Console = new();

    public static void DisplayHeader()
    {
        Console.PrintNewLine(ProductionContext.Product + " " + ProductionContext.Version, (int)ConsoleColor.Blue);

        Console.PrintNewLine(ProductionContext.Description, (int)ConsoleColor.Green);

        Console.PrintNewLine("Par : " + ProductionContext.Developer, (int)ConsoleColor.Red);

        Console.PrintNewLine("Licence : " + ProductionContext.License, (int)ConsoleColor.Magenta);

        Console.PrintNewLine("Build Version : " + ProductionContext.BuildNumber, (int)ConsoleColor.Green);

        Console.PrintNewLine("Lien du répertoire du code source : " + ProductionContext.RepositoryUri,
            (int)ConsoleColor.Blue);
    }

    public static void DisplayMenu(List<object> content) 
    {
        Console.PrintNewLine("List of Options");
        content.ForEach(
            item => { Console.PrintNewLine(item.ToString() ?? "N/A"); }
        ); 
        Console.PrintNewLine("Choose a option : ", (int) ConsoleColor.Cyan);
    }
}