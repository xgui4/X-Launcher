namespace X_Launcher_Core.Model
{
    public class ConfigFile
    {
        public GameConfig GameConfig { get; }
        public LauncherConfig LauncherConfig { get; }
        public SessionInfo SessionInfo { get; }

        public ConfigFile(GameConfig gameConfigToCopy, LauncherConfig launcherConfigToCopy, SessionInfo sessionInfoToCopy)
        {
            GameConfig = gameConfigToCopy;
            LauncherConfig = launcherConfigToCopy;
            SessionInfo = sessionInfoToCopy;
        }
    }
}
