using BoardGameAPI.Config;
using BoardGameAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((_, configBuilder) =>
{
    configBuilder.AddEnvironmentVariables();
    configBuilder.AddJsonFile("local.settings.json", true, true);
});

var services = builder.Services;
var configuration = builder.Configuration;

// CONFIGURE SERVICES
services.AddDbContext<BoardGameAPIContext>((configBuilder) =>
{
    configBuilder.UseSqlServer(configuration.GetConnectionString(ConnectionStrings.Database));
});
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// CONFIGURE APP
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();