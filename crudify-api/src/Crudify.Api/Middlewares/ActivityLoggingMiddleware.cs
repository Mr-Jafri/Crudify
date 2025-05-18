
using System.Text.Json;

namespace Crudify.Api.Middlewares;

public class ActivityLoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ILoggingService logger)
    {
        try
        {
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));

            await next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();

            using var document = JsonDocument.Parse(responseText);
            int statusCode = document.RootElement.GetProperty("httpStatusCode").GetInt32();

            context.Response.StatusCode = statusCode;
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var log = new ActivityLog
            {
                UserId = context.User?.Identity?.Name ?? "Anonymous",
                Action = "API Request",
                Endpoint = context.Request.Path,
                HttpMethod = context.Request.Method,
                RequestBody = requestBody,
                ResponseBody = responseText,
                StatusCode = context.Response.StatusCode,
                IPAddress = context.Connection.RemoteIpAddress.ToString()
            };

            await logger.LogActivityAsync(log);
            await responseBody.CopyToAsync(originalBodyStream);

            
        }
        catch (Exception ex)
        {
            var exception = new ExceptionLog
            {
                ExceptionMessage = ex.Message,
                Path = context.Request.Path,
                StackTrace = ex.StackTrace,
                Source = ex.Source
            };

            await logger.LogExceptionAsync(exception);
        }
    }
}
