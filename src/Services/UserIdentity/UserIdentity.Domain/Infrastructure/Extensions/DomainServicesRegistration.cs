using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace UserIdentity.Domain.Infrastructure.Extensions;

public static class DomainServicesRegistration
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}