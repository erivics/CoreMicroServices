using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOcelot();
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
