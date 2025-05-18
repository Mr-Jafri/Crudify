namespace Crudify.Api.Middlewares;

public class ExceptionLoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ILoggingService logger)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var log = new ExceptionLog
            {
                UserId = context.User?.Identity?.Name ?? "Anonymous",
                ExceptionMessage = ex.Message,
                StackTrace = ex.StackTrace,
                Source = ex.Source,
                Path = context.Request.Path,
                QueryString = context.Request.QueryString.ToString()
            };

            await logger.LogExceptionAsync(log);

            context.Response.StatusCode = context.Response is not null ? context.Response.StatusCode : (int)HttpStatusCode.InternalServerError;
            if (context.Response.StatusCode is (int)HttpStatusCode.Unauthorized)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized Access.");
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred.");
            }
        }
    }
}
