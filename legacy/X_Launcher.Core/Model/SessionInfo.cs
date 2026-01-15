using System.Security.Cryptography;
using System.Text;

public class SessionInfo
{
    public Guid Id { get; private set; }
    public string? Name { get; private set; }

    /*
    public string? MicrosoftEmailAccount { get; private set; }
    public string? HashedPassword { get; private set; }
    */
    
    public SessionInfo()
    {
        Id = Guid.NewGuid();
        Name = "Dev"; // temps
    }

    public SessionInfo(string name /*, string microsoftEmailAccount, string password */)
    {
        Id = Guid.NewGuid();
        Name = name;
        // MicrosoftEmailAccount = microsoftEmailAccount;
        // HashedPassword = HashPassword(password);
    }

    /* 
    private static string HashPassword(string password)
    {
       to be re-created later
    }
    */

    public void FillUserInfo(string name, string microsoftEmailAccount, string password)
    {
        Name = name;
        /*
        MicrosoftEmailAccount = microsoftEmailAccount;
        HashedPassword = HashPassword(password);
        */
    }
}