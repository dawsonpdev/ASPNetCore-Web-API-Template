using Serilog;
using LoggerConfigurationExtensions = WebApiTemplate.Logging.Extensions.LoggerConfigurationExtensions;

namespace WebApiTemplate.WebApi;

public static class Program
{
    public static async Task<int> Main()
    {
        const string appName = "WebApiTemplate";
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        LoggerConfigurationExtensions.SetupLogger(environmentName);

        try
        {
            Log.Information("Starting web host {AppName}", appName);
            await CreateHostBuilder().Build().RunAsync();
            Log.Information("Ending web host {AppName}", appName);
            return 0;
            
        }
        catch (Exception e) when (e is not HostAbortedException)
        {
            Log.Fatal(e, "Host terminated unexpectedly {AppName}", appName);
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .UseSerilog(Log.Logger, dispose: true)
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}