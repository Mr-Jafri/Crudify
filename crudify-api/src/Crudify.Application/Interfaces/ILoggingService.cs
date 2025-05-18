
namespace Crudify.Application.Interfaces;

public interface ILoggingService
{
    Task LogActivityAsync(ActivityLog log);
    Task LogExceptionAsync(ExceptionLog log);
}
