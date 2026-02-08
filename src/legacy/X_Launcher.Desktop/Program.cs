using System;
using System.Runtime.InteropServices;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Microsoft.Extensions.Logging;
using X_Launcher.Views;
using X_Launcher;
using XboxAuthNet.Game.Msal;
using Handlers;

namespace X_Launcher.Desktop
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
            }
            catch (Exception ex)
            {
                CrashHandler(ex); 
            }
        }

        private static void CrashHandler(Exception e)
        {
            GuiHandler handler = new GuiHandler();
            handler.Error(e.ToString());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Critical Error : \n {e.ToString()}");
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        private static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
        }
    }
}