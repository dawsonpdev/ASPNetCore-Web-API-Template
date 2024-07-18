using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace WebApiTemplate.Logging.Extensions;

public static class LoggerConfigurationExtensions
{
    public static void SetupLogger(string? environmentName)
    {
        var baseConfiguration = new LoggerConfiguration()
            .ConfigureBaseLogging(environmentName);

        Log.Logger = baseConfiguration.CreateLogger();
    }

    private static LoggerConfiguration ConfigureBaseLogging(this LoggerConfiguration loggerConfiguration,
        string? environmentName)
    {
        return environmentName switch
        {
            "Development" => loggerConfiguration.ConfigureDevelopmentLogging(),
            "Production" => loggerConfiguration.ConfigureProductionLogging(),
            _ => loggerConfiguration.ConfigureProductionLogging()
        };
    }

    private static LoggerConfiguration ConfigureDevelopmentLogging(this LoggerConfiguration loggerConfiguration)
    {
        return loggerConfiguration
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .WriteTo.Async(a => a.Console(theme: AnsiConsoleTheme.Code,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message:lj}{NewLine}{Exception:j}"))
            .Enrich.FromLogContext();
    }

    private static LoggerConfiguration ConfigureProductionLogging(this LoggerConfiguration loggerConfiguration)
    {
        return loggerConfiguration
            .MinimumLevel.Warning()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
            .WriteTo.Async(a => a.Console(theme: AnsiConsoleTheme.Code,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message:lj}{NewLine}{Exception:j}"))
            .Enrich.FromLogContext();
    }
}