using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Handlers;
using X_Launcher_Core.Model;
using X_Launcher_Core;
using X_Launcher_Core.Handlers;


namespace X_Launcher.ViewModels;

public partial class HeaderViewModel(IDisplayHandler displayHandler) : ObservableRecipient
{
    private IDisplayHandler _displayHandler = displayHandler;

    public HeaderViewModel() : this(new GuiHandler())
    {
    }
    
    [ObservableProperty]
    private string _title = ProductionContext.Product;

    [ObservableProperty] 
    private string? _element1 = FeaturesExtension.GetValue(Features.Play);
    
    [ObservableProperty] 
    private string? _element2 = FeaturesExtension.GetValue(Features.Install);
    
    [ObservableProperty] 
    private string? _element3 = FeaturesExtension.GetValue(Features.SaveConfig);

    [ObservableProperty] 
    private string? _element4 = FeaturesExtension.GetValue(Features.Setting);

    [RelayCommand]
    private void SetElement1()
    {
        _displayHandler ??= new GuiHandler();
        _displayHandler.WarnAsync("Not implemented yet");
    }

    [RelayCommand]
    private void SetElement2()
    {
        _displayHandler ??= new GuiHandler();
        _displayHandler.WarnAsync("Not implemented yet");
    }

    [RelayCommand]
    private void SetElement3()
    {
        _displayHandler ??= new GuiHandler();
        _displayHandler.WarnAsync("Not implemented yet");
    }

    [RelayCommand]
    private void SetElement4()
    {
        _displayHandler ??= new GuiHandler();
        _displayHandler.WarnAsync("Not implemented yet");
    }
}