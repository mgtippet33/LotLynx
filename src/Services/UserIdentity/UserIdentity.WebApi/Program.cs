using Microsoft.EntityFrameworkCore;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Reflection;
using UserIdentity.Data;
using UserIdentity.Domain.Contracts.Services;
using UserIdentity.Domain.Infrastructure.Settings;
using UserIdentity.Domain.Services;
using UserIdentity.WebApi.Infrastructure.Web;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var databaseSettings = builder.Configuration.GetSection(DatabaseSettings.SectionName).Get<DatabaseSettings>();

services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(databaseSettings!.ConnectionString);
});

services.BuildServiceProvider()!.GetService<AppDbContext>()!.Database.Migrate();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddAutoMapper(Assembly.GetExecutingAssembly());
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IOAuthAuthorizationServerProvider, AuthorizationServerProvider>();
services.AddScoped<IOAuthBearerAuthenticationProvider, HeaderOAuthBearerProvider>(sp => new HeaderOAuthBearerProvider("Access-Token"));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOwin(setup =>
{
    setup(nextApp =>
    {
        var appBuilder = new AppBuilder();

        appBuilder.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
        {
            AllowInsecureHttp = true,

            TokenEndpointPath = new Microsoft.Owin.PathString("/token"),
            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(10),

            Provider = services.BuildServiceProvider()!.GetService<IOAuthAuthorizationServerProvider>()
        });

        appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
        {
            Provider = services.BuildServiceProvider()!.GetService<IOAuthBearerAuthenticationProvider>()
        });

        appBuilder.Run(async owinContext =>
        {
            var aspNetCoreEnv = new Microsoft.AspNetCore.Http.Features.FeatureCollection(owinContext.Environment);
            await nextApp(aspNetCoreEnv);
        });

        return appBuilder.Build<Func<IDictionary<string, object>, Task>>();
    });
});

app.UseAuthorization();

app.MapControllers();

app.Run();
