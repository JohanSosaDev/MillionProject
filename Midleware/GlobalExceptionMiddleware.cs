using System.Net;
using System.Text.Json;

namespace PropertyApi.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        try
        {
            await _next(ctx);
        }
        catch (Exception ex)
        {
            var error = ex;
            ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ctx.Response.ContentType = "application/json";
            var payload = JsonSerializer.Serialize(new { message = "Unexpected error." });
            await ctx.Response.WriteAsync(payload);
        }
    }
}

