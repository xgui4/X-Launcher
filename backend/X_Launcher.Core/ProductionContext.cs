using System.Globalization;

namespace X_Launcher_Core;

public static class ProductionContext
{
    public const string Version = "Prototype (branch develop)";
    public const string Developer = "Xgui4 Studio";
    public const string Product = "X Launcher Core";
    public const string License = "MIT License";
    public const string Description = "A cross-platform FOSS Minecraft Launcher Backend written in C#";
    public const string InternetRequired = "Internet Required";
    public const string AppIdNotRegistered = "App ID not registered";
    public const string AppIdNotFound = "App ID not found";
    private static readonly string BuildDate = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(" ", "_");
    public static readonly string BuildNumber = BuildDate + "+" + Version;
    public static readonly Uri RepositoryUri = new("https://github.com/xgui4/X-launcher");
}