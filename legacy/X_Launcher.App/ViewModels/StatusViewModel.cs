using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using X_Launcher.ViewModel;
namespace X_Launcher.ViewModels;

public partial class StatusViewModel : ObservableRecipient, IRecipient<LoginEvent>
{
    [ObservableProperty]
    private string _status = "Unregistered";

    public StatusViewModel()
    {
        Messenger.RegisterAll(this); 
    }

    public void Receive(LoginEvent message)
    {
        Status = $"{message.Username} logged in";
    }

    public void Receive()
    {
        Status = "Unregistered";
    }
}

