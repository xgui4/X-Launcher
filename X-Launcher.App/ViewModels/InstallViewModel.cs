using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Handlers;
using X_Launcher_Core.Handlers;
using X_Launcher_Core.Model;
using X_Launcher_Core.Service;
using X_Launcher_Core.Utility;
using X_Launcher.ViewModel;

namespace X_Launcher.ViewModels;

public partial class InstallViewModel(IDisplayHandler displayHandler) : ObservableRecipient
{
    private IDisplayHandler _displayHandler;
    private readonly MinecraftLauncherService _launcher;

    public InstallViewModel() : this(new GuiHandler())
    {
        _displayHandler = new GuiHandler();
        _launcher = new MinecraftLauncherService(_displayHandler);
        _ = InitializeComboBoxAsync();
    }

    [ObservableProperty]
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

    [RelayCommand] private async Task SetInstaller()
    {
        if (!_launcher.SessionIsValid())
        {
            await _displayHandler.UserInteractionAsync("Current Sessions is invalid! Do you want to retry the connection? Yes or No.", null, "Session Handler");
            await _launcher.AuthenticateAsync();
        }
        _displayHandler ??= new GuiHandler();
        var game = new GameConfig(SelectedVersion ?? GameConfig.DefaultVersion, LauncherPath ?? AppContext.BaseDirectory + "/.minecraft", MaxRam ?? GameConfig.DefaultMaximumMemory, MinRam ?? GameConfig.DefaultMinimumMemory);
        var launcherObject = new LauncherConfig(game.Path ?? GameConfig.DefaultVersion);
        var user = new SessionInfo(Username ?? "Dev");
        MinecraftLauncherService service = new(game, launcherObject, user, _displayHandler);
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
                GameConfig.DefaultVersion,
            ];
        }
    }
}