using HtmlAgilityPack;
using X_Launcher_CLI.ViewModels;
using X_Launcher_Core.Utility;

namespace X_Launcher_Core.Model;

public static class FeaturesExtension
{
    public static string GetValue(Features action)
    {
        return action switch
        {
            Features.Play => "1. Play",
            Features.Install => $"2. Install {(!InternetStatus.IsConnected() ? "\u001b[31m[Required Internet]\u001b[0m" : "")}",
            Features.SaveConfig => "3. Save Configuration",
            Features.Setting => "4. Setting",
            Features.Quit => "5. Quit",
            _ => throw new ArgumentOutOfRangeException("This argument was out of bound!")
        };
    }
}