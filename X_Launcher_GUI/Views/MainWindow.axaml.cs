using Avalonia.Controls;
using Avalonia.Interactivity;
using X_Launcher_Core;
using X_Launcher_GUI.ViewModels;

namespace X_Launcher_GUI.Views;

public partial class MainWindow : Window
{
    // À améliorer lorsque j'aurais trouver une meilleur méthode pour le faire
    public MainWindow()
    {
        InitializeComponent();
        LoginButton.Click += LoginButton_Click; 
    }
    
    private static void LoginButton_Click(object? sender, RoutedEventArgs e)
    {
        MainWindowViewModel.Greeting = "Welcome, Xgui4, To X Launcher Core !"; 
    } 
}