namespace X_Launcher_Core;

public class User
{
    public string Username { get;  }

    public bool IsLoggedIn { get; }

    public bool HasAccess { get; }
    
    public bool IsBanned { get; }

    private User(string username, bool isLoggedIn, bool hasAccess, bool isBanned)
    {
        this.Username = username;
        this.IsLoggedIn = isLoggedIn;
        this.HasAccess = hasAccess;
        this.IsBanned = isBanned;
    }

    public static User CreateInstance(string username, bool isLoggedIn, bool hasAccess, bool isBanned)
    {
        return new User(username, isLoggedIn, hasAccess, isBanned);
    }

    public override string ToString()
    {
        return $"Username: {this.Username}, Is logged in: {this.IsLoggedIn}, Has access: {this.HasAccess}, Is banned: {this.IsBanned}.";
    }

}