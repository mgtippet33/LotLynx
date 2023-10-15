using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Serilog;
using UserIdentity.Domain.Infrastructure.Exceptions;

namespace UserIdentity.Infrastructure.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(context, ex);
            Log.Error($"{ex}");
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex is NotFoundUserException
            ? (int)HttpStatusCode.BadRequest
            : (int)HttpStatusCode.InternalServerError;
        
        var result = JsonSerializer.Serialize(new { error = ex.Message });
        
        return context.Response.WriteAsync(result);
    }
}