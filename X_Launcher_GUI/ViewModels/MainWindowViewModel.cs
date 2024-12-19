using X_Launcher_Core;

namespace X_Launcher_GUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public static string Greeting { get; set; } = ProductionContext.WelcomeMessage;
}