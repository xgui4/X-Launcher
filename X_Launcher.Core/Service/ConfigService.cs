using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using X_Launcher_Core.Handlers;
using X_Launcher_Core.Model;

namespace X_Launcher_Core.Service
{
    public class ConfigService
    {
        private readonly IDisplayHandler _displayHandler; 

        public ConfigService(IDisplayHandler displayHandler)
        {
            _displayHandler = displayHandler; 
        }
        public void SaveConfig(ConfigFile configFile, string nameOfTheConfig)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "X-Launcher", "saves");

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            string configJson = JsonSerializer.Serialize(configFile, options);
            
            string fullPath = Path.Combine(path, nameOfTheConfig);

            File.WriteAllText(fullPath, configJson); 

            _displayHandler.Info($"The configFile {nameOfTheConfig} have been saved in the path {fullPath}");
        }
    }
}
