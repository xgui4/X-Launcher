using CmlLib.Core.Auth;
using CmlLib.Core.ProcessBuilder;
using CmlLib.Core;
using X_Launcher_Core.Model;

public class MinecraftLauncherService
{
    private readonly Game Game;
    private readonly Launcher Launcher;
    private readonly User User; 

    public MinecraftLauncherService(Game game, Launcher launcher, User user)
    {
        Game = game;
        Launcher = launcher;
        User = user;
    }
     
    public async Task LaunchOfflineAsync()
    {
        // Initialize the launcher
        var path = new MinecraftPath(Game.Path);
        var launcher = new MinecraftLauncher(Launcher.Path);

        // Add event handlers
        launcher.FileProgressChanged += (sender, args) =>
        {
            Console.WriteLine($"Name: {args.Name}");
            Console.WriteLine($"Type: {args.EventType}");
            Console.WriteLine($"Total: {args.TotalTasks}");
            Console.WriteLine($"Progressed: {args.ProgressedTasks}");
        };

        launcher.ByteProgressChanged += (sender, args) =>
        {
            Console.WriteLine($"{args.ProgressedBytes} bytes / {args.TotalBytes} bytes");
        };

        // Get all versions
        var versions = await launcher.GetAllVersionsAsync();
        foreach (var v in versions)
        {
            Console.WriteLine(v.Name);
        }

        // Install and launch the game
        await launcher.InstallAsync(Game.Version);
        var process = await launcher.BuildProcessAsync(Game.Version, new MLaunchOption
        {
            Session = MSession.CreateOfflineSession(User.Name),
            MaximumRamMb = Game.MaxMemory,
            MinimumRamMb = Game.MinMemory,
        });
        process.Start();
    }
}