using Microsoft.AspNetCore.Builder;
using UserIdentity.Infrastructure.Middlewares;

namespace UserIdentity.Infrastructure.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionHandlerMiddleware>();

        return builder;
    }
}