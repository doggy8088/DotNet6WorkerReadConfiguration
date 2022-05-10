using Serilog;
using Serilog.Builder;
using Serilog.Builder.Models;
using w1;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureHostConfiguration(x =>
{
    x.AddJsonFile("settings.json", false, true);
    x.AddJsonFile($"settings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true);
    x.AddEnvironmentVariables();
});

IHost host = builder
    .ConfigureServices((ctx, services) =>
    {
        // Get Connection String
        ctx.Configuration.GetConnectionString("DefaultConnection");

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
