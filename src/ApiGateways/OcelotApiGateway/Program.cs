using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOcelot();

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json",true, true);

//builder.Host.ConfigureAppConfiguration(configDelegate =>
//{
//    configDelegate.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json",true, true);
//});

builder.Services.AddLogging(configure =>
{
    configure.AddConfiguration(builder.Configuration.GetSection("Logging"));
    configure.AddConsole();
    configure.AddDebug();
});

var app = builder.Build();

await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
