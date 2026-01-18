using X_Launcher_Core;
using X_Launcher_Core.Handlers;
using X_Launcher_Core.Service;
using X_Launcher.Service.Handlers;

namespace X_Launcher.Service
{
    public class Program
    {
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

            app.MapGet("/x_launcher.core.service", () =>
            {
                IDisplayHandler displayHandler = new ConsoleHandlers(); 
                MinecraftLauncherService service = new MinecraftLauncherService(displayHandler);

                service.GetAllVersions(); 

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
        string app_name, 
        string app_version, 
        string developper_name, 
        string license, 
        string description, 
        string build_number
    );
}
