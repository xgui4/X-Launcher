using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using X_Launcher_Core.Handlers;
using X_Launcher_Core.Model;
using X_Launcher_Core.Service;
using X_Launcher_Core.Utility;
using X_Launcher.ViewModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Handlers;
using Avalonia.Controls;
using Avalonia;

namespace X_Launcher.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
    private IDisplayHandler _displayHandler;
    private readonly MinecraftLauncherService _launcher;

    public MainViewModel()
    {
        _displayHandler = new GuiHandler();
        _launcher = new MinecraftLauncherService(_displayHandler);
        InitializeComboBoxAsync().ConfigureAwait(false);
    }
    public MainViewModel(GuiHandler displayHandler)
    {
        _displayHandler = displayHandler;
        _launcher = new MinecraftLauncherService(_displayHandler);
        InitializeComboBoxAsync().ConfigureAwait(false);
    }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string? _username;
    
    [ObservableProperty]
    private ObservableCollection<string> _versions = []; 
    
    [ObservableProperty] 
    private string? _selectedVersion;
    
    [ObservableProperty] 
    private string? _launcherPath;

    [ObservableProperty]
    private int? _minRam;

    [ObservableProperty] 
    private int? _maxRam;

    [ObservableProperty]
    private string? _loginLabel = "Login";

    [ObservableProperty]
    private string? _loginButtonColor = "Green"; 
    
    [RelayCommand]
    private async Task Login()
    {
        if (LoginLabel == "Login")
        {
            await _launcher.AuthenticateAsync();
            await Task.Delay(1000);
            if (_launcher.SessionIsValid())
            {
                LoginLabel = "Logout";
                LoginButtonColor = "Red";
                Messenger.Send(new LoginEvent(_launcher.GetMsName() ?? Username ?? "Undefined"));
            }
        }
        else if (LoginLabel == "Logout")
        { 
            await _launcher.LogoutAsync();
            await Task.Delay(1000);
            LoginLabel = "Login";
            LoginButtonColor = "Green";
            Messenger.Send(new LogoutEvent());
        }
    }

    private bool CanLaunch()
    {
        return true; // _launcher.SessionIsValid(); temporaly disable due to not working proprely  
    }

    [RelayCommand(CanExecute = nameof(CanLaunch))]
    private async Task SetLauncher()
    {
        if (_launcher.SessionIsValid() == false)
        {
            // temporaly disabled due to no working properly right now !
            /*
            await _displayHandler.UserInteractionAsync("Current Sessions is invalid! Do you want to retry the connection? Yes or No.", null, "Session Handler");
            await _launcher.AuthenticateAsync();
            */
        }
        _displayHandler ??= new GuiHandler();
        var game = new GameConfig(SelectedVersion ?? GameConfig.DefaultVersion, LauncherPath ?? AppContext.BaseDirectory + "/.minecraft", MinRam ?? 4000, MaxRam ?? 2000);
        var launcherObject = new LauncherConfig(game.Path ?? GameConfig.DefaultVersion);
        var user = new SessionInfo(Username ?? "Dev");
        MinecraftLauncherService service = new(game, launcherObject, user ,_displayHandler);
        try
        {
            await service.LaunchOnlineSessionAsync();
        }
        catch (Exception ex)
        {
            await _displayHandler.ErrorAsync(ex.ToString());
            await _launcher.LaunchOfflineSessionAsync(); 
        } 
    }

    [RelayCommand]
    private void SearchLocally()
    {
        _displayHandler = new GuiHandler();
        _displayHandler.WarnAsync("This feature isn't implemented yet.");
    }

    private async Task InitializeComboBoxAsync()
    {
        await SeedComboBox();
    }

    private async Task SeedComboBox()
    {
        if (InternetStatus.IsConnected())
        {
            var versions = await _launcher.GetAllVersions();
            Versions = new ObservableCollection<string>(versions.Select(version => version.Name));
            if (!versions.Any())
            {
                Versions.Add("Error while getting versions!");
            }
        }
        else
        {
            Versions =
            [
                "1.21.4",
            ];
        }
    }
}