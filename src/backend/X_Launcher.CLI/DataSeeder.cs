using X_Launcher_CLI.ViewModels;
using X_Launcher_Core.Model;

namespace X_Launcher_CLI;

public class DataSeeder
{
    /// <summary>
    ///     The list that containt the info of the domain
    /// </summary>
    private readonly List<object> _seed = []; // Object est temporaire ... 

    /// <summary>
    ///     Constructeur des éléments a ajouter
    /// </summary>
    /// <param name="element1"></param>
    /// <param name="element2"></param>
    /// <param name="element3"></param>
    /// <param name="element4"></param>
    /// <param name="element5"></param>
    /// <param name="element6"></param>
    public DataSeeder(string element1, string element2, string element3, string element4, string element5, string element6)
    {
        _seed.Add(element1);
        _seed.Add(element2);
        _seed.Add(element3);
        _seed.Add(element4);
        _seed.Add(element5);
        _seed.Add(element6);
    }

    /// <summary>
    ///     Default Constructor (temporaly)
    /// </summary>
    public DataSeeder()
    {
        List<string> actions = Enum.GetValues(typeof (Features))
            .Cast<Features>()
            .Select(action => FeaturesExtension.
            GetValue(action)).ToList();

        foreach (var action in actions) 
            { 
            _seed.Add(action);
        }
    }

    /// <summary>
    ///     Copy constructor
    /// </summary>
    /// <param name="src"></param>
    public DataSeeder(DataSeeder src)
    {
        _seed.AddRange(src._seed);
    }

    /// <summary>
    /// Seed the list provided in the parameter with the internal list
    /// </summary>
    /// <param name="list"> the list to fill with the dataseeder </param>
    /// <returns>The filled list </returns>
    public List<object> SeedList(List<object> list)
    {
        list.AddRange(_seed);
        return list;
    }
}