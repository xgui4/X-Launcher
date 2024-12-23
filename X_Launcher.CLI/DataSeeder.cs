using System;
using System.Collections.Generic;
using X_Launcher_Core;

namespace X_Launcher_CLI
{
    public readonly struct DataSeeder
    {
        /// <summary>
        /// The list that containt the info of the domain
        /// </summary>
        private readonly List<Object> _seed = []; // Object est temporaire ... 

        /// <summary>
        /// Constructeur des éléments a ajouter
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
        /// Default Constructor (temporaly)
        /// </summary>
        public DataSeeder()
        {
            _seed.Add("1. Play");
            _seed.Add("2. Install");
            _seed.Add("3. Setting");
            _seed.Add("4. About");
            _seed.Add("5. License");
            _seed.Add("6. Quit");
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="src"></param>
        public DataSeeder(DataSeeder src)
        {
            _seed.AddRange(src._seed);
        }

        public List<Object> SeedList(List<Object> list)
        {
            list.AddRange(_seed);
            return list;
        }
    }
}
