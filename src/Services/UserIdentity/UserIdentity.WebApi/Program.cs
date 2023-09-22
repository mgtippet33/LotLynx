using Microsoft.EntityFrameworkCore;
using UserIdentity.Data;
using UserIdentity.Domain.Infrastructure.Settings;

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
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
