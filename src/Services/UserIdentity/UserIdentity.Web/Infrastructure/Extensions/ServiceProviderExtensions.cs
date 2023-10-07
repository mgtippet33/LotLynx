using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Data;
using UserIdentity.Data.Entities;
using UserIdentity.Domain.Contracts.Services;
using UserIdentity.Domain.Infrastructure.Extensions;
using UserIdentity.Domain.Infrastructure.Settings;
using UserIdentity.Domain.Services;
using UserIdentity.Web.Infrastructure.Configurations;

namespace UserIdentity.Web.Infrastructure.Extensions;

public static class ServiceProviderExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection(DatabaseSettings.SectionName).Get<DatabaseSettings>();

        services
            .AddDatabaseContextServices(databaseSettings!)
            .AddIdentityServerServices()
            .AddCustomServices()
            .AddDomainServices()
            .RegisterSettings(configuration);
        
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
    
    private static IServiceCollection AddIdentityServerServices(this IServiceCollection services)
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

        services.ConfigureApplicationCookie(config =>
        {
            config.Cookie.Name = "LotLynx.Identity.Cookies";
            config.LoginPath = "/Auth/Login";
            config.LogoutPath = "/Auth/Logout";
        });

        services
            .AddIdentityServer()
            .AddAspNetIdentity<User>()
            .AddInMemoryApiResources(AuthConfiguration.ApiResources)
            .AddInMemoryIdentityResources(AuthConfiguration.IdentityResources)
            .AddInMemoryApiScopes(AuthConfiguration.ApiScopes)
            .AddInMemoryClients(AuthConfiguration.Clients)
            .AddDeveloperSigningCredential();

        return services;
    }

    private static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }

    private static IServiceCollection RegisterSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ReturnUrlsSettings>(options => configuration.GetSection(ReturnUrlsSettings.SectionName).Bind(options));

        return services;
    }
}