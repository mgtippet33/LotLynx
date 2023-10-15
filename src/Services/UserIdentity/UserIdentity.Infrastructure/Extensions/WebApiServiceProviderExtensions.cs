using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserIdentity.Data;
using UserIdentity.Data.Entities;
using UserIdentity.Domain.Contracts.Services;
using UserIdentity.Domain.Infrastructure.Extensions;
using UserIdentity.Domain.Services;
using UserIdentity.Infrastructure.Settings;

namespace UserIdentity.Infrastructure.Extensions;

public static class WebApiServiceProviderExtensions
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection(DatabaseSettings.SectionName).Get<DatabaseSettings>();
        var authenticationSettings = configuration.GetSection(AuthenticationApiSettings.SectionName).Get<AuthenticationApiSettings>();

        services
            .AddDatabaseContextServices(databaseSettings!)
            .AddCustomServices()
            .AddDomainServices()
            .AddAuthenticationSettings(authenticationSettings!);
        
        return services;
    }

    private static IServiceCollection AddDatabaseContextServices(this IServiceCollection services, DatabaseSettings databaseSettings)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(databaseSettings!.ConnectionString);
        });
        
        services.BuildServiceProvider().GetService<AppDbContext>()!.Database.Migrate();
        
        return services;
    }
    
    private static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }

    private static IServiceCollection AddAuthenticationSettings(this IServiceCollection services, AuthenticationApiSettings settings)
    {
        services.AddIdentity<User, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 5;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;

            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
        services
            .AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = settings.Authority;
                options.Audience = settings.Audience;
                options.RequireHttpsMetadata = false;
            });

        return services;
    }
}