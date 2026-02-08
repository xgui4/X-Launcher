namespace X_Launcher_Core.Handlers;

public interface IDisplayHandler
{
    /// <summary>
    ///  Display in a new line a message with a specified color
    /// </summary>
    /// <param name="message">The string to show</param>
    /// <param name="color">The color to show , default to 0</param>
    void PrintNewLine(string message, int color = 15);

    /// <summary>
    ///  Display a message with a specified color
    /// </summary>
    /// <param name="message">The string to show</param>
    /// <param name="color">The color to show , default to 0</param>
    void Print(string message, int color = 15);

    /// <summary>
    ///  Reset the color of the display interface
    /// </summary>
    void ResetColor();

    /// <summary>
    ///     Refresh the display
    /// </summary>
    void Refresh();

    /// <summary>
    ///  Display or log a error in red
    /// </summary>
    /// <param name="errorMessage">The error message to show</param>
    void Error(string errorMessage);

    /// <summary>
    /// Display or log a warning message
    /// </summary>
    /// <param name="warningMessage">The warning message to show</param>
    void Warn(string warningMessage);

    /// <summary>
    ///  Display or log a debug infomation
    /// </summary>
    /// <param name="infoMessage">The Warning message to show</param>
    void Info(string infoMessage);
    /// <summary>
    ///  Display or log a debug infomation in a async way
    /// </summary>
    /// <param name="message">Message to show</param>
    /// <param name="color">Color of the message</param>
    /// <returns>The task</returns>
    Task InfoAsync(string message, int color = 15);
    
    /// <summary>
    /// Dislay or log a warning in a async way
    /// </summary>
    /// <param name="warningMessage">Warning to show</param>
    /// <returns>The Task</returns>
    Task WarnAsync(string warningMessage);
    
    /// <summary>
    /// Dipslay or log a debug a Error in a async way
    /// </summary>
    /// <param name="errorMessage"></param>
    /// <returns>The Task</returns>
    Task ErrorAsync(string errorMessage);

    /// <summary>
    /// Display a userInteraction box, with a optinal Enum for action and a option custom boxTitle
    /// </summary>
    /// <param name="message">Message to show</param>
    /// <param name="actions">Optional : custom box action </param>
    /// <param name="boxTitle">Title for the box</param>
    /// <param name="paramSupp">A reserve for framework of a other parameter</param>
    /// <returns></returns>
    Task<int> UserInteractionAsync(string message, Enum? actions, string? boxTitle);
}