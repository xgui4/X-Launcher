using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using X_Launcher_Core.Handlers;
using X_Launcher.ViewModels;
using X_Launcher.Views;
using Handlers;

namespace X_Launcher;

public partial class App : Application
{
    private GuiHandler? displayHandler;
    public override void Initialize()
    {
        displayHandler = new GuiHandler();
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(displayHandler ?? new GuiHandler())
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel(displayHandler ?? new GuiHandler()),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
