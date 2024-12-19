using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_Launcher_Core;

namespace X_Launcher_CLI.Views
{
    public class MenuView
    {
        public MenuView() { }

        public static void Display()
        {
            ConsoleDisplay.PrintNewLine(ProductionContext.Product + " " + ProductionContext.Version, ConsoleDisplayColor.Blue);

            ConsoleDisplay.PrintNewLine(ProductionContext.Description, ConsoleDisplayColor.Green);

            ConsoleDisplay.PrintNewLine("Par : " + ProductionContext.Developer, ConsoleDisplayColor.Red);

            ConsoleDisplay.PrintNewLine("Licence : " + ProductionContext.License, ConsoleDisplayColor.Magenta);

            ConsoleDisplay.PrintNewLine("Build Version : " + ProductionContext.BuildNumber, ConsoleDisplayColor.Green); 

            ConsoleDisplay.PrintNewLine("Lien du répertoire du code source : " + ProductionContext.RepositoryUri.ToString(), ConsoleDisplayColor.Blue);
        }
    }
}
