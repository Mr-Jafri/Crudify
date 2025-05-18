
namespace Crudify.Application.Services;

public class LoggingService(ApplicationContext context) : ILoggingService
{
    public async Task LogActivityAsync(ActivityLog log)
    {
        context.ActivityLogs.Add(log);
        await context.SaveChangesAsync();

    }

    public async Task LogExceptionAsync(ExceptionLog log)
    {
        context.ExceptionLogs.Add(log);
        await context.SaveChangesAsync();
    }
}
