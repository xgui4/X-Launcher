using X_Launcher_Core.Handlers;

namespace X_Launcher_Cli.Model;

public class ConsoleDisplay : IDisplayHandler
{
    private const ConsoleColor DefaultColor = ConsoleColor.Gray;
    private const ConsoleColor ErrorColor = ConsoleColor.Red;
    private const ConsoleColor WarningColor = ConsoleColor.Yellow;
    private const ConsoleColor InfoColor = ConsoleColor.White;

    public void PrintNewLine(string message, int color = 15)
    {
        Console.ForegroundColor = (ConsoleColor)color;
        Console.WriteLine(message);
    }

    public void Print(string message, int color = 15)
    {
        Console.ForegroundColor = (ConsoleColor)color;
        Console.Write(message);
    }

    public void ResetColor()
    {
        Console.ResetColor();
    }

    public void Refresh()
    {
        Console.Clear();
        ResetColor();
    }

    public void Error(string errorMessage)
    {
        Console.ForegroundColor = ErrorColor;
        Console.WriteLine($"Error : {errorMessage}");
        Console.ForegroundColor = DefaultColor;
    }

    public void Warn(string warningMessage)
    {
        Console.ForegroundColor = WarningColor;
        Console.WriteLine($"Warning : {warningMessage}");
        Console.ForegroundColor = DefaultColor;
    }

    public void Info(string infoMessage)
    {
        Console.ForegroundColor = InfoColor;
        Console.WriteLine($"Information : {infoMessage}");
        Console.ForegroundColor = DefaultColor;
    }

    public async Task InfoAsync(string message, int color = 15)
    {
        Console.ForegroundColor = InfoColor;
        await Console.Out.WriteLineAsync($"Info : {message}");
        Console.ForegroundColor = DefaultColor;
    }

    public async Task WarnAsync(string warningMessage)
    {
        Console.ForegroundColor = WarningColor;
        await Console.Out.WriteLineAsync($"Warning : {warningMessage}");
        Console.ForegroundColor = DefaultColor;
    }

    public async Task ErrorAsync(string errorMessage)
    {
        Console.ForegroundColor = ErrorColor;
        await Console.Out.WriteLineAsync($"Error : {errorMessage}");
        Console.ForegroundColor = DefaultColor;
    }

    public Task<int> UserInteractionAsync(string message, Enum? actions, string? boxTitle)
    {
        throw new NotImplementedException();
    }
}