using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using X_Launcher.ViewModels;

namespace X_Launcher.Views;

public partial class HeaderView : UserControl
{
    public HeaderView()
    {
        InitializeComponent();
        DataContext = new HeaderViewModel();
    }
}