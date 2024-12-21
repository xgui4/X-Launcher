using X_Launcher_CLI.Views;
using X_Launcher_Core;

namespace X_Launcher_CLI; 

public class Program
{
    public static void Main(string[] args)
    {
        ConsoleDisplay.ChangeBackgroundColor(ConsoleDisplayColor.White);

        MenuView.Display();

        ConsoleDisplay.ResetColor();
    }
}