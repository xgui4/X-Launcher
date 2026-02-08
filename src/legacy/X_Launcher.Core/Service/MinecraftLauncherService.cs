using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Auth.Microsoft;
using CmlLib.Core.ProcessBuilder;
using CmlLib.Core.VersionLoader;
using CmlLib.Core.VersionMetadata;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using X_Launcher_Core.Exception;
using X_Launcher_Core.Handlers;
using X_Launcher_Core.Model;
using X_Launcher_Core.Utility;
using XboxAuthNet.Game.Msal;

namespace X_Launcher_Core.Service;

/// <summary>
/// Service for launching Minecraft via the CmlLib library
/// </summary>
public class MinecraftLauncherService
{
    private GameConfig _gameConfig;
    private LauncherConfig _launcherConfig;
    private SessionInfo _sessionInfo;
    private readonly IDisplayHandler _displayHandler;
    private MinecraftLauncher _minecraftLauncher;
    private MSession _session; 

    /// <summary>
    /// A default minecraft launcher service with the required DI
    /// <param name="displayHandler"/> DI for the displayHandler interface </param>
    /// </summary>
    public MinecraftLauncherService(IDisplayHandler displayHandler)
    {
        _gameConfig = new GameConfig();
        _launcherConfig = new LauncherConfig();
        _sessionInfo = new SessionInfo();
        _minecraftLauncher = new MinecraftLauncher();
        _displayHandler = displayHandler; 
        _session = new MSession();
    }
    
    /// <summary>
    /// Constructor for creating the service
    /// </summary>
    /// <param name="gameConfig">The gameConfig config (GameConfig object)</param>
    /// <param name="launcherConfig">The launcherConfig specific config (LauncherConfig object)</param>
    /// <param name="sessionInfo">The sessionInfo information (SessionInfo object)</param>
    /// <param name="displayHandler">The display handler implementation for display of info, error or warning </param>
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

    /// <summary>
    ///  Game config setter
    /// </summary>
    /// <param name="gameConfig">Game config object</param>
    public void SetGameConfig(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
    }

    /// <summary>
    /// Launcher config setter and Minecraft Launcher Config updater
    /// </summary>
    /// <param name="launcherConfig">Launcher config object</param>
    public void SetLauncherConfig(LauncherConfig launcherConfig)
    {
        _launcherConfig = launcherConfig;
        UpdateLauncherConfig();
    }

    /// <summary>
    /// Session info setter
    /// </summary>
    /// <param name="sessionInfo">Section info object</param>
    public void SetSessionInfo(SessionInfo sessionInfo)
    {
        _sessionInfo = sessionInfo;
    }

    /// <summary>
    /// Authenticate the launcher session with a Microsoft account
    /// </summary>
    ///<param name="displayHandlerEnabled">If set to true, activate the displayHandler interface, 
    /// else the diaplayHandler Interface will not be used </param>
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

    /// <summary>
    /// Disconnect the MSession and clear cache from webview 2 browser, right now it only supported Windows
    /// </summary>
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

    /// <summary>
    /// Launch the gameConfig via the config and sessionInfo info given by the constructor
    /// </summary>
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
    
    /// <summary>
    /// Launch the gameConfig without Online functionality with an already installed version
    /// <exception cref="ApplicationException">Exception that was emitted in case the launch failed due to a unhandled exception</exception>
    /// </summary>
    public async Task LaunchOfflineSessionAsync()
    {
        try
        {
            var path = new MinecraftPath(_gameConfig.Path ?? throw new MclsArgumentException("GameConfig path was null"));
            var parameters = MinecraftLauncherParameters.CreateDefault(path);

            // load only the locally installed version 
            parameters.VersionLoader = new LocalJsonVersionLoader(path);

            // initialize a new launcherConfig with parameters
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

    /// <summary>
    /// Get all Minecraft versions from Mojang server (require internet)
    /// </summary>
    /// <returns>The Collection of metadata of Minecraft versions </returns>
    /// <exception cref="NoInternetConnectionException">If the internet connection isnt working</exception>
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

    /// <summary>
    /// This a method who verify if the microsoft session is valid or not (if the player have the correct license to play Minecraft)
    /// </summary>
    /// <returns> true if session is valid, false is sessions isn't valid </returns>
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

    /// <summary>
    /// Get the MSession username
    /// </summary>
    /// <returns>The string of the MSession username</returns>
    public string GetMsName()
    {
        return _session.Username ?? "Unregistred"; 
    }
}
