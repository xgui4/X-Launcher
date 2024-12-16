using X_Launcher_Core;

namespace X_Launcher_CLI; 

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to X-Launcher!");
        
        var test = User.CreateInstance("Test", false, false , false);

        Console.WriteLine(test.ToString()); 
    }
}