namespace X_Launcher_Core.Exception;

/// <summary>
/// Exception given when a method need internet but the internet is not connected
/// </summary>
public class NoInternetConnectionException : ApplicationException
{
    /// <summary>
    /// Base Exception 
    /// </summary>
    public NoInternetConnectionException() : base() { }
    
    /// <summary>
    /// Exception with a messsage
    /// </summary>
    /// <param name="message">Debug info</param>
    public NoInternetConnectionException(string message) : base(message) { }
    
    /// <summary>
    /// Exception with a message that was caused by a other exception
    /// </summary>
    /// <param name="message">Debug info</param>
    /// <param name="inner">Exception that caused this exception</param>
    public NoInternetConnectionException(string message, System.Exception inner) : base(message, inner) { }
}