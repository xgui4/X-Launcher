using X_Launcher_Core.Handlers;

namespace X_Launcher_Core.Model;

public static class FeaturesExtension
{
    public static string GetValue(Features action)
    {
        return action switch
        {
            Features.Play => "1. Play",
            Features.Install => $"2. Install {(!InternetStatus.IsConnected() ? "\e[31m[Required Internet]\e[0m" : "")}",
            Features.SaveConfig => "3. Save Configuration",
            Features.Setting => "4. Setting",
            Features.Login => "5. Login",
            Features.Quit => "6. Quit",
            _ => throw new ArgumentOutOfRangeException("This argument was out of bound!")
        };
    }
    
}