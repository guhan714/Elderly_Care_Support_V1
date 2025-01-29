using System.Text.Json;
using ElderlyCareSupport.Application.Contracts.Response;

namespace ElderlyCareSupport.Server.Middleware;

public sealed class GlobalErrorHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var response = new
            {
                success = false,
                statusCode = context.Response.StatusCode,
                data = Enumerable.Empty<string>(),
                errorMessage = e.Message,
                errors = new List<Error>{new (e.Message)}
            };
            
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}