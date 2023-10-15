using UserIdentity.Infrastructure.Extensions;
using LoggerExtensions = UserIdentity.Infrastructure.Extensions.LoggerExtensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddWebApiServices(builder.Configuration);

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

LoggerExtensions.InitializeLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddlewares();

app.MapControllers();

app.Run();