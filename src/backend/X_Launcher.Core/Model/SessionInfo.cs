namespace X_Launcher_Core.Model;

public class SessionInfo(string name = "Dev", string minecraftEmailAccount = "demo", string? minecraftPassword = null)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = name;
    public string MicrosoftEmailAccount { get; private set; } = minecraftEmailAccount;
    public string? HashedPassword { get; private set; } = minecraftPassword != null ? HashPassword(minecraftPassword) : null;
    public void UpdateUserInfo(string name, string microsoftEmailAccount, string password)
    {
        Name = name;
        MicrosoftEmailAccount = microsoftEmailAccount;
        HashedPassword = HashPassword(password);
    }
    
    private static string HashPassword(string password)
    {   
        // Todo -> Hashed the password
        return password;
    }

}