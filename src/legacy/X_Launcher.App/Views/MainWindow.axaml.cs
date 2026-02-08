using Avalonia.Controls;
using X_Launcher_Core.Handlers;
using X_Launcher.ViewModels;
using Handlers;

namespace X_Launcher.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        GuiHandler guiHandler = new GuiHandler();
        DataContext = new MainViewModel(guiHandler);
        LauncherControl.DataContext = new StatusViewModel();
    }
}