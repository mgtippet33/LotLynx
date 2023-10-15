using Serilog;

namespace UserIdentity.Infrastructure.Extensions;

public static class LoggerExtensions
{
    public static void InitializeLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Warning()
            .WriteTo.File("Logs/UserIdentity-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}