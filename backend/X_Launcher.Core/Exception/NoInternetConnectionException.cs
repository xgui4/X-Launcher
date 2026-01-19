namespace X_Launcher_Core.Exception;

public class NoInternetConnectionException : ApplicationException
{
    public NoInternetConnectionException() : base() { }
    
    public NoInternetConnectionException(string message) : base(message) { }
    
    public NoInternetConnectionException(string message, System.Exception inner) : base(message, inner) { }
}