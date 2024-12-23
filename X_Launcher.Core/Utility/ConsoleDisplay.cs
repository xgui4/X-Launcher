using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace X_Launcher_Core.Utility
{
    public class ConsoleDisplay
    {
        private const ConsoleDisplayColor defaultFGColor = ConsoleDisplayColor.Default;
        private const ConsoleDisplayColor defaultBGColor = ConsoleDisplayColor.Black;

        private ConsoleDisplay()
        {
            Console.BackgroundColor = (ConsoleColor)defaultBGColor;
        }

        public static void PrintNewLine(string message, ConsoleDisplayColor color = defaultFGColor)
        {
            Console.ForegroundColor = (ConsoleColor)color;
            Console.WriteLine(message);
        }

        public static void Print(string message, ConsoleDisplayColor color = defaultFGColor)
        {
            Console.ForegroundColor = (ConsoleColor)color;
            Console.Write(message);
        }

        public static void ChangeBackgroundColor(ConsoleDisplayColor color)
        {
            Console.BackgroundColor = (ConsoleColor)color;
        }

        public static void ResetColor()
        {
            Console.ForegroundColor = (ConsoleColor)defaultFGColor;
            Console.BackgroundColor = (ConsoleColor)defaultBGColor;
        }

        public static void Refresh()
        {
            Console.Clear();
            ResetColor();
        }
    }
}
