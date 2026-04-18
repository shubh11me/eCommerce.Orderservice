using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json",optional:false,reloadOnChange:true);

builder.Services.AddOcelot();
var app = builder.Build();
//app.MapGet("/health", () => "Health");
await app.UseOcelot();

app.Run();
