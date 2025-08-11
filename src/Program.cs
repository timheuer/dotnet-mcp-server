using McpServerTemplate.Prompts;
using McpServerTemplate.Tools;
#if (TransportMethod == "stdio")
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

#endif
#if (EnableOpenTelemetry)
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
#endif
#if (TransportMethod == "streamable-http")
// Create a web application builder for HTTP transport
var builder = WebApplication.CreateBuilder(args);
#else
var builder = Host.CreateApplicationBuilder(args);
#endif

// Register the MCP server and add the tools/prompts from the current assembly
builder.Services.AddMcpServer()
#if (EnableHttpTransport)
    .WithHttpTransport()
#else
    .WithStdioServerTransport()
#endif
    .WithTools<SampleTools>()
    .WithPrompts<SamplePrompts>();

#if (EnableHttpTransport)
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
// Configure logging for better integration with MCP clients (stdio)
builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Trace;
});

await builder.Build().RunAsync();
#endif
