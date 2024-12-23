using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_Launcher_Core.Model
{
    public class Game
    {
        public Guid Id { get; private set; }
        public string Version { get; private set; }
        public string Path { get; private set; }
        public string? Client {  get;  private set; }
        public string? Server { get; private set; }
        public int MaxMemory { get; private set; }
        public int MinMemory { get; private set; }

        public Game(string version, string path, int maxMemory, int minMemory, string? client = null, string? server = null) { 
            Id = Guid.NewGuid();
            Version = version;
            Path = path;
            Client = client;
            Server = server;
            MaxMemory = maxMemory;
            MinMemory = minMemory;
        }
    }
}
