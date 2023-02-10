using BlazorServerTest.Data.Exceptions;
using System.Net;

namespace BlazorServerTest.Middlewares;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, exception.Message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            DataNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.NotFound,
        };

        await context.Response.WriteAsJsonAsync(exception.Message);
    }
}
