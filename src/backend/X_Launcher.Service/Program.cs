using X_Launcher_Core;
using X_Launcher_Core.Handlers;
using X_Launcher_Core.Service;
using X_Launcher.Service.Handlers;
using Microsoft.AspNetCore.Http.Features;

namespace X_Launcher.Service
{
    public class Program
    {
        public const string ServerURL = "/x_launcher.core.service";
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.MapGet(ServerURL, async () =>
            {
                IDisplayHandler displayHandler = new ConsoleHandlers(); 
                MinecraftLauncherService service = new MinecraftLauncherService(displayHandler);

                await service.GetAllVersions(); 

                await service.LaunchDemo("Demo"); 

                return new MinecraftLauncherString(
                    ProductionContext.Product,
                    ProductionContext.Version,
                    ProductionContext.Developer,
                    ProductionContext.License,
                    ProductionContext.Description,
                    ProductionContext.BuildNumber
                );
            })
            .WithName("GeAppString");

            await app.RunAsync(); 
        }
    }

    public record MinecraftLauncherString
    (
        string AppName, 
        string AppVersion, 
        string DevelopperName, 
        string License, 
        string Description, 
        string BuildNumber
    );
}
