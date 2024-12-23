using System.Security.Cryptography;
using System.Text;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string MicrosoftEmailAccount { get; private set; }
    public string HashedPassword { get; private set; }

    public User(string name, string microsoftEmailAccount, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        // MicrosoftEmailAccount = microsoftEmailAccount;
        // HashedPassword = HashPassword(password);
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}

