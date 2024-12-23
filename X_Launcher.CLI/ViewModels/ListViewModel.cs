using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_Launcher_CLI.ViewModels
{
    public class ListViewModel
    {
        public List<Object> List {get; private set;}

        public ListViewModel(DataSeeder seeder) {
            List = [];
            List = seeder.SeedList(List); 
        }
    }
}