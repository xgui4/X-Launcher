namespace X_Launcher_Core
{
    public class ProductionContext
    {        
        public const string Version = "Dev Build";
        public const string Developer = "Xgui4 Studio";
        public const string Product = "X Launcher Core";
        public const string License = "MIT License";
        public const string Description = "Un lanceur Minecraft fonctionnelle FOSS écrit en C#, qui suit le MVVM " +
            "et qui supporte plusieurs plateformes de bureau(Windows et Linux officiellement).";

        public static readonly string BuildDate = DateTime.Now.ToString().Replace(" ", "_");
        public static readonly string BuildNumber = BuildDate + "+" + Version;
        public static readonly Uri RepositoryUri = new("https://github.com/xgui4/x-launcher");

        public readonly List<string> mainMenuOptions = [];

        public ProductionContext()
        {
            mainMenuOptions.Add("Play");
            mainMenuOptions.Add("Install");
            mainMenuOptions.Add("Setting");
            mainMenuOptions.Add("About");
            mainMenuOptions.Add("License");
            mainMenuOptions.Add("Quit");
        }
    }
}