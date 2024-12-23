using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Threading.Tasks;
using X_Launcher.ViewModel;

namespace X_Launcher.ViewModels;

public partial class MainViewModel : ObservableRecipient
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
        Console.WriteLine("Le login n'a pas été encore implementer !");
        Messenger.Send(new LoginEvent(Username)); 
    }

    private bool CanLogin() => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

    [RelayCommand]
    private void SetDemo(string username)
    {
        Greeting = $"Welcome, {username}, To X Launcher Core !";
    }
}