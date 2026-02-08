namespace X_Launcher_CLI.ViewModels;

public class ListViewModel
{
    public ListViewModel(DataSeeder seeder)
    {
        List = seeder.SeedList(List ?? []);
    }

    public List<object> List { get; }
}