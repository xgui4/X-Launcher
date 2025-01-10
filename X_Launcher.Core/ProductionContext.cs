namespace X_Launcher_Core;

public static class ProductionContext
{
    public const string Version = "Dev Build";
    public const string Developer = "Xgui4 Studio";
    public const string Product = "X Launcher Core";
    public const string License = "MIT License";
    public const string Description = "A functional FOSS Minecraft launcherConfig written in C#, which follows MVVM (for the GUI) and supports multiple desktop platforms (Windows and Linux officially).";
    public const string InternetRequired = "Internet Required";
    public const string AppIdNotRegistered = "App ID not registered";
    public const string AppIdNotFound = "App ID not found";
    public static readonly string BuildDate = DateTime.Now.ToString().Replace(" ", "_");
    public static readonly string BuildNumber = BuildDate + "+" + Version;
    public static readonly Uri RepositoryUri = new("https://github.com/xgui4/x-launcherConfig");
}