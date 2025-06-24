using Microsoft.Extensions.DependencyInjection;
#if (EnableHttpTransport)
using Microsoft.AspNetCore.Builder;
#else
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
#endif
#if (EnableOpenTelemetry)
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
#endif

#if (EnableHttpTransport)
// Create a web application builder for HTTP transport
var builder = WebApplication.CreateBuilder(args);

// Configure services and MCP server with HTTP transport
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

// Build the web application
var app = builder.Build();

// Map the MCP endpoint
app.MapMcp();

// Run the web application (defaults to port 5000 for HTTP, 5001 for HTTPS)
await app.RunAsync();
#else
// Create a host builder for dependency injection, logging, and configuration
var builder = Host.CreateDefaultBuilder(args);

// Configure logging for better integration with MCP clients
builder.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Information);
});

// Configure services and MCP server
builder.ConfigureServices((context, services) =>
{
    // Register the MCP server, configure it to use stdio transport,
    // and scan the assembly for tool and prompt definitions
    services.AddMcpServer()
        .WithStdioServerTransport()
        .WithToolsFromAssembly()
        .WithPromptsFromAssembly();
});

// Build and run the host, which starts the MCP server
var host = builder.Build();
await host.RunAsync();
#endif
