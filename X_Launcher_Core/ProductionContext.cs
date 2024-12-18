namespace X_Launcher_Core
{
    public struct ProductionContext
    {
        public const string Version = "Dev Build";
        public const string Developer = "Xgui4 Studio";
        public const string Product = "X Launcher Core";
        public const string License = "MIT License";
        public static readonly Uri RepositoryUri = new Uri("https://github.com/xgui4/x-launcher");

        public ProductionContext()
        {
        }
    }
}