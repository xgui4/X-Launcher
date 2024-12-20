using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace X_Launcher_GUI.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string? _username;


    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]

    private string? _password;

    partial void OnPasswordChanged(string? value)
    {
        LoginCommand.NotifyCanExecuteChanged();
    }

    [ObservableProperty]
    private string _greeting = "Welcome To X Launcher Core !";

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task Login()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        Greeting = "Login Sucess !"; 
    }

    private bool CanLogin() => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

    [RelayCommand]
    private void SetDemo(string username)
    {
        Greeting = $"Welcome, {username}, To X Launcher Core !";
    }
}