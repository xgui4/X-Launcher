using Avalonia.Controls;
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