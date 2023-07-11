var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(configure =>
{
    configure.AddConfiguration(builder.Configuration.GetSection("Logging"));
    configure.AddConsole();
    configure.AddDebug();
});

var app = builder.Build();



app.MapGet("/", () => "Hello World!");

app.Run();
