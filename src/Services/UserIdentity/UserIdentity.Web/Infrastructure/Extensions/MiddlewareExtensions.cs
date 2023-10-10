using UserIdentity.Web.Infrastructure.Middlewares;

namespace UserIdentity.Web.Infrastructure.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionHandlerMiddleware>();

        return builder;
    }
}