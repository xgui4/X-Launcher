using CmlLib.Core;

namespace X_Launcher_Core.Model;

public class GameConfig
{
    public const int DefaultMaximumMemory = 4000;
    public const int DefaultMinimumMemory = 0;
    public const string DefaultVersion = "1.21.4"; 
    private static readonly string DefaultMinecraftPath = MinecraftPath.GetOSDefaultPath();
    public Guid Id { get; private set; }
    public string? Version { get; private set; }
    public string? Path { get; private set; }
    public string? Client { get; private set; }
    public string? Server { get; private set; }
    public int? MaxMemory { get; private set; }
    public int? MinMemory { get; private set; }
    
    public GameConfig()
    {
        Id = Guid.NewGuid();
    }

    public GameConfig(string version, string? path = null, int maxMemory = DefaultMaximumMemory, int? minMemory = null,
        string? client = null, string? server = null)
    {
        Id = Guid.NewGuid();
        Version = version;
        Path = path ?? DefaultMinecraftPath;
        Client = client;
        Server = server;
        MaxMemory = maxMemory;
        MinMemory = minMemory;
    }
    public void FillGameConfig(string? version, string? path, string? client, string? server, int? maxMemory,
        int? minMemory)
    {
        Version = version ?? DefaultVersion;
        Path = path ?? DefaultMinecraftPath;
        Client = client ?? null;
        Server = server ?? null;
        MaxMemory = maxMemory ?? DefaultMaximumMemory;
        MinMemory = minMemory ?? DefaultMinimumMemory;
    }
}