using X_Launcher_Core;

namespace X_Launcher_CLI; 

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(ProductionContext.Product + " " + ProductionContext.Version);
        
        Console.WriteLine("Par : " + ProductionContext.Developer);
        
        Console.WriteLine("Licence : " + ProductionContext.License);

        var link = ProductionContext.RepositoryUri; 
        
        Console.WriteLine("Lien du répertoire du code source : " + link.ToString());

    }
}