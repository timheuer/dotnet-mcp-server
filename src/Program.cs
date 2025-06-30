using McpServerTemplate.Prompts;
using McpServerTemplate.Tools;
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
    .WithTools<SampleTools>()
    .WithPrompts<SamplePrompts>();

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
var builder = Host.CreateApplicationBuilder(args);

// Register the MCP server, configure it to use stdio transport,
// and add the tools/prompts from the current assembly
builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<SampleTools>()
    .WithPrompts<SamplePrompts>();

// Configure logging for better integration with MCP clients
builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Trace;
});

await builder.Build().RunAsync();
#endif
