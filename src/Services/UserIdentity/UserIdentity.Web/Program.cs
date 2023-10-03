using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Data;
using UserIdentity.Data.Entities;
using UserIdentity.Domain.Contracts.Services;
using UserIdentity.Domain.Infrastructure.Settings;
using UserIdentity.Domain.Services;
using UserIdentity.Web.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

var databaseSettings = builder.Configuration.GetSection(DatabaseSettings.SectionName).Get<DatabaseSettings>();

var services = builder.Services;
// Add services to the container.
services.AddControllersWithViews();

services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(databaseSettings!.ConnectionString);
});

services.AddIdentity<User, IdentityRole>(config =>
{
    config.Password.RequiredLength = 4;
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

services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IUserService, UserService>();

services.BuildServiceProvider().GetService<AppDbContext>()!.Database.Migrate();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
