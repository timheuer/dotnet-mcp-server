using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace McpServerTemplate;

/// <summary>
/// Entry point for the MCP Server application.
/// </summary>
public class Program
{
    /// <summary>
    /// Main entry point that configures and starts the MCP server.
    /// </summary>
    /// <param name="args">Command line arguments</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public static async Task Main(string[] args)
    {
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
    }
}
