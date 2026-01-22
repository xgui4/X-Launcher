namespace X_Launcher_Core.Handlers;

public interface IDisplayHandler
{
    void PrintNewLine(string message, int color = 15);

    void Print(string message, int color = 15);
    
    void ResetColor();
    
    void Refresh();
    
    void Error(string errorMessage);
    
    void Warn(string warningMessage);
    
    void Info(string infoMessage);

    Task InfoAsync(string message, int color = 15);
    
    Task WarnAsync(string warningMessage);
    
    Task ErrorAsync(string errorMessage);
    
    Task<int> UserInteractionAsync(string message, Enum? actions, string? boxTitle);
}