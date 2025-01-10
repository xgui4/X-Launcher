using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using X_Launcher_Core.Handlers;

namespace Handlers;

public class GuiHandler : IDisplayHandler
{
    public void PrintNewLine(string message, int color = 15)
    {
        Debug.WriteLine(message);
    }

    public void Print(string message, int color = 15)
    {
        Debug.Write(message);
    }

    public void ResetColor()
    {
        throw new NotImplementedException();
    }

    public void Refresh()
    {
        throw new NotImplementedException();
    }

    public void Error(string errorMessage)
    {
        _ = ErrorAsync(errorMessage);
    }

    public void Warn(string warningMessage)
    {
        _ = WarnAsync(warningMessage);
    }

    public void Info(string infoMessage)
    {
        _ = InfoAsync(infoMessage);
    }

    public async Task InfoAsync(string message, int color = 15)
    {
        var box = MessageBoxManager.GetMessageBoxStandard("Information", message);

        await box.ShowAsync();
    }

    public async Task WarnAsync(string warningMessage)
    {
        var box = MessageBoxManager.GetMessageBoxStandard("Warning", warningMessage);

        await box.ShowAsync();
    }

    public async Task ErrorAsync(string errorMessage)
    {
        var box = MessageBoxManager.GetMessageBoxStandard("Error", errorMessage);

        await box.ShowAsync();
    }

    public async Task<int> UserInteractionAsync(string message, Enum? actions, string? boxTitle)
    {
        var box = MessageBoxManager
          .GetMessageBoxStandard(boxTitle ?? "Question", message,
             ButtonEnum.YesNo);

        var result = await box.ShowAsync();

        return (int)result; 
    }
}