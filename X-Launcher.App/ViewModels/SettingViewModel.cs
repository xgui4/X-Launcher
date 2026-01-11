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

public partial class SettingViewModel(IDisplayHandler displayHandler) : ObservableRecipient
{
    private IDisplayHandler _displayHandler;
    private readonly MinecraftLauncherService _launcher;

    public SettingViewModel() : this(new GuiHandler())
    {
        _displayHandler = new GuiHandler();
        _launcher = new MinecraftLauncherService(_displayHandler);
    }
    
}