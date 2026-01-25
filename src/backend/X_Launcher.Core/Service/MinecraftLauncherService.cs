using System.Threading.Tasks;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Auth.Microsoft;
using CmlLib.Core.ProcessBuilder;
using CmlLib.Core.VersionLoader;
using CmlLib.Core.VersionMetadata;
using X_Launcher_Core.Exception;
using X_Launcher_Core.Handlers;
using X_Launcher_Core.Model;
using XboxAuthNet.Game.Msal;

namespace X_Launcher_Core.Service;

public class MinecraftLauncherService
{
    private GameConfig _gameConfig;
    private LauncherConfig _launcherConfig;
    private SessionInfo _sessionInfo;
    private readonly IDisplayHandler _displayHandler;
    private MinecraftLauncher _minecraftLauncher;
    private MSession _session; 

    public MinecraftLauncherService(IDisplayHandler displayHandler)
    {
        _gameConfig = new GameConfig();
        _launcherConfig = new LauncherConfig();
        _sessionInfo = new SessionInfo();
        _minecraftLauncher = new MinecraftLauncher();
        _displayHandler = displayHandler; 
        _session = new MSession();
    }
    
    public MinecraftLauncherService(GameConfig gameConfig, LauncherConfig launcherConfig, SessionInfo sessionInfo, IDisplayHandler displayHandler)
    {
        _gameConfig = gameConfig;
        _launcherConfig = launcherConfig;
        _sessionInfo = sessionInfo;
        _displayHandler = displayHandler;
        _minecraftLauncher = new MinecraftLauncher(); 
        _session = new MSession(); 
    }
    private void UpdateLauncherConfig()
    {
        if (_launcherConfig.Path is not null) _minecraftLauncher = new MinecraftLauncher(_launcherConfig.Path);
    }
    
    public void SetGameConfig(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
    }
    
    public void SetLauncherConfig(LauncherConfig launcherConfig)
    {
        _launcherConfig = launcherConfig;
        UpdateLauncherConfig();
    }
    
    public void SetSessionInfo(SessionInfo sessionInfo)
    {
        _sessionInfo = sessionInfo;
    }
    
    public async Task AuthenticateAsync(bool displayHandlerEnabled = false)
    {
        try
        {
            if (InternetStatus.IsConnected())
            {
                await MicrosoftLoginHandlerAsync();
            }
            else
            { 
                if (displayHandlerEnabled) await _displayHandler.WarnAsync("Impossible to authenticate without a active internet connection.");
            }
        }
        catch (System.Exception ex)
        {
            if (displayHandlerEnabled) await _displayHandler.ErrorAsync(ex.Message);
        }
    }
    
    public async Task LogoutAsync()
    {
        try
        {
            if (OperatingSystem.IsWindows())
            {
                var loginHandler = JELoginHandlerBuilder.BuildDefault();
                await loginHandler.SignoutWithBrowser();
            }
            else
            {
                _displayHandler.Warn("This feature isn't supported with msal yet!");
            }
        }
        catch (System.Exception ex)
        {
            await _displayHandler.ErrorAsync(ex.Message);
        }
    }

    private async Task MicrosoftLoginHandlerAsync()
    {
        var loginHandler = JELoginHandlerBuilder.BuildDefault();
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            await OathWebview2AuthenficationHandlerAsync(loginHandler);
        }
        else
        {
            await MsalAuthenficationHandlerAsync(loginHandler);
        }
    }

    private async Task MsalAuthenficationHandlerAsync(JELoginHandler loginHandler)
    {
        var app = await MsalClientHelper.BuildApplicationWithCache(
            Environment.GetEnvironmentVariable("MSAL_APPLICATION_ID") ??
            throw new ApplicationException("MSAL_APPLICATION_ID was null"));
        var authenticator = loginHandler.CreateAuthenticatorWithNewAccount(default);
        authenticator.AddMsalOAuth(app, msal => msal.Interactive());
        authenticator.AddXboxAuthForJE(xbox => xbox.Basic());
        authenticator.AddJEAuthenticator();
        _session = await authenticator.ExecuteForLauncherAsync();
        if (SessionIsValid())
        {
            await _displayHandler.InfoAsync($"{_session.Username} Connected!", (int)ConsoleColor.Green);
            await _displayHandler.InfoAsync($"SessionInfo Type : {_session.UserType}", (int)ConsoleColor.Green);
        }
    }

    private async Task OathWebview2AuthenficationHandlerAsync(JELoginHandler loginHandler)
    {
        try
        {
            _session = await loginHandler.AuthenticateSilently();
            if (SessionIsValid())
            {
                await _displayHandler.InfoAsync($"{_session.Username} Connected!", (int)ConsoleColor.Green);
                await _displayHandler.InfoAsync($"SessionInfo Type : {_session.UserType}", (int)ConsoleColor.Green);
            }
        }
        catch
        {
            await _displayHandler.WarnAsync(
                "Failed to authenticate silently. Retrying Authentication interactively");
            _session = await loginHandler.AuthenticateInteractively();
            if (SessionIsValid())
            {
                await _displayHandler.InfoAsync($"{_session.Username} Connected!", (int)ConsoleColor.Green);
                await _displayHandler.InfoAsync($"SessionInfo Type : {_session.UserType}", (int)ConsoleColor.Green);
            }
        }
    }


    public async Task LaunchOnlineSessionAsync()
    {
        if (!SessionIsValid() && InternetStatus.IsConnected())
        {
            await AuthenticateAsync(); 
        }
        try
        {
            if (InternetStatus.IsConnected())
            {
                var process = await _minecraftLauncher.InstallAndBuildProcessAsync(_gameConfig.Version ?? throw new MclsArgumentException("version was null"),
                    new MLaunchOption
                    {
                        Session = _session,
                        MaximumRamMb = _gameConfig.MaxMemory ?? GameConfig.DefaultMaximumMemory,
                        MinimumRamMb = _gameConfig.MinMemory ?? 0
                    });
                process.Start();
            }
        }
        catch (System.Exception ex)
        {
            await _displayHandler.ErrorAsync(ex.Message);
        }
    }

    public async Task LaunchDemo(string username = "demo")
    {
        var launcher = new MinecraftLauncher();

        await launcher.InstallAsync("1.21.1");

        var launchOption = new MLaunchOption
        {
            Session = MSession.CreateOfflineSession(username),
            IsDemo = true
        };

        await launcher.BuildProcessAsync("1.21.1", launchOption); 
    }

    public async Task LaunchOfflineSessionAsync()
    {
        try
        {
            var path = new MinecraftPath(_gameConfig.Path ?? throw new MclsArgumentException("GameConfig path was null"));
            var parameters = MinecraftLauncherParameters.CreateDefault(path);

            parameters.VersionLoader = new LocalJsonVersionLoader(path);

            var launcher = new MinecraftLauncher(parameters);

            var process = await launcher.BuildProcessAsync(
                _gameConfig.Version ?? throw new MclsArgumentException("GameConfig version was null"), new MLaunchOption
                {
                    Session = MSession.CreateOfflineSession(_sessionInfo.Name ??
                                                            throw new MclsArgumentException("Username was full")),
                    MaximumRamMb = _gameConfig.MaxMemory ?? GameConfig.DefaultMaximumMemory,
                    MinimumRamMb = _gameConfig.MinMemory ?? 0
                });
            process.Start();
        }
        catch (System.Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
    
    public async Task<VersionMetadataCollection> GetAllVersions()
    {
        if (InternetStatus.IsConnected())
        {
            return await _minecraftLauncher.GetAllVersionsAsync();
        }
        else
        {
            throw new NoInternetConnectionException("Cannot get all version from Mojang server while connection is off");
        }
    }
    
    public bool SessionIsValid()
    {
        try
        {
            // _displayHandler.InfoAsync(_session.CheckIsValid().ToString());
            return _session.CheckIsValid();
        }
        catch (System.Exception ex)
        {
            _displayHandler.Error($"A error occured while checking the session : \n {ex.Message}");
            return false;
        }
    }

    public string GetMsName()
    {
        return _session.Username ?? "Unregistred"; 
    }
}
