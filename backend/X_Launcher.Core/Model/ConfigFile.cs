using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
