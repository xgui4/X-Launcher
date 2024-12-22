using Avalonia.Controls;
using Avalonia.Controls.Platform;
using X_Launcher.ViewModels;

namespace X_Launcher.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        LauncherControl.DataContext = new StatusViewModel(); 
    }
}
