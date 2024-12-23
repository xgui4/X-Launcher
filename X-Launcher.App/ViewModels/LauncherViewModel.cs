using CommunityToolkit.Mvvm.ComponentModel;
using X_Launcher.Views;

namespace X_Launcher.ViewModels;

public partial class LauncherViewModel : ObservableRecipient
{
    [ObservableProperty]
    public string _name = ""; 
}