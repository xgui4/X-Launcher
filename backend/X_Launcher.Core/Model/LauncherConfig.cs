namespace X_Launcher_Core.Model;

public class LauncherConfig
{
    public Guid Id { get; private set; }
    public string? Path { get; private set; }
    public LauncherConfig()
    {
        Id = Guid.NewGuid();
    }

    public LauncherConfig(string path)
    {
        Id = Guid.NewGuid();
        Path = path;
    }

    public void FillLauncherConfig(string path)
    {
        Path = path;
    }
}