using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_Launcher_Core.Model
{
    public class Launcher
    {
        public Guid Id { get; private set; }
        public string Path { get; private set; }
        public Launcher(string path) {
            Id= Guid.NewGuid();
            Path = path;
        }
    }
}
