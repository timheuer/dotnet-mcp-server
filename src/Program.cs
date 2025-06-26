#if (!EnableHttpTransport)
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

#endif
#if (EnableOpenTelemetry)
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

#endif
#if (EnableHttpTransport)
var builder = WebApplication.CreateBuilder(args);

// Register the MCP server, configure it to use HTTP transport,
// and add the tools/prompts from the current assembly
builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly()
    .WithPromptsFromAssembly();

#if (EnableOpenTelemetry)
builder.Services.AddOpenTelemetry()
    .WithTracing(b => b.AddSource("*")
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation())
    .WithMetrics(b => b.AddMeter("*")
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation())
    .WithLogging()
    .UseOtlpExporter();
#endif

var app = builder.Build();
app.MapMcp();
await app.RunAsync();
#else
var builder = Host.CreateDefaultBuilder(args);

// Configure logging for better integration with MCP clients
builder.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Information);
});

builder.ConfigureServices((context, services) =>
{
    // Register the MCP server, configure it to use stdio transport,
    // and add the tools/prompts from the current assembly
    services.AddMcpServer()
        .WithStdioServerTransport()
        .WithToolsFromAssembly()
        .WithPromptsFromAssembly();
});

var host = builder.Build();
await host.RunAsync();
#endif
