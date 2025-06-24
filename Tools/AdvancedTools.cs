using System.ComponentModel;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace McpServerTemplate.Tools;

/// <summary>
/// Advanced MCP tools demonstrating dependency injection and more complex scenarios.
/// These examples show how to use services and perform more sophisticated operations.
/// </summary>
[McpServerToolType]
public static class AdvancedTools
{
    /// <summary>
    /// Demonstrates how to use dependency injection with an ILogger service.
    /// </summary>
    /// <param name="logger">Injected logger service</param>
    /// <param name="message">Message to log</param>
    /// <param name="level">Log level</param>
    /// <returns>Confirmation of log entry</returns>
    [McpServerTool(Name = "log_message")]
    [Description("Logs a message with the specified level using dependency injection")]
    public static string LogMessage(
        ILogger<Program> logger,
        [Description("The message to log")] string message,
        [Description("The log level (Information, Warning, Error)")] string level = "Information")
    {
        var logLevel = level switch
        {
            "Warning" => LogLevel.Warning,
            "Error" => LogLevel.Error,
            "Debug" => LogLevel.Debug,
            _ => LogLevel.Information
        };

        logger.Log(logLevel, "MCP Tool Message: {Message}", message);
        return $"Logged message at {level} level: {message}";
    }

    /// <summary>
    /// Demonstrates an async tool that performs a delayed operation.
    /// </summary>
    /// <param name="seconds">Number of seconds to wait</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result after the delay</returns>
    [McpServerTool(Name = "delayed_operation")]
    [Description("Performs an async operation with a specified delay")]
    public static async Task<string> DelayedOperation(
        [Description("Number of seconds to wait")] int seconds,
        CancellationToken cancellationToken = default)
    {
        if (seconds < 0 || seconds > 30)
        {
            return "Error: Delay must be between 0 and 30 seconds";
        }

        await Task.Delay(TimeSpan.FromSeconds(seconds), cancellationToken);
        return $"Completed after waiting {seconds} second(s)";
    }

    /// <summary>
    /// Demonstrates working with complex return types and data processing.
    /// </summary>
    /// <param name="data">Comma-separated numbers</param>
    /// <returns>Statistical analysis of the data</returns>
    [McpServerTool(Name = "calculate_statistics")]
    [Description("Calculates statistics for a list of comma-separated numbers")]
    public static object CalculateStatistics(
        [Description("Comma-separated list of numbers")] string data)
    {
        try
        {
            var numbers = data.Split(',')
                             .Select(s => double.Parse(s.Trim()))
                             .ToArray();

            if (numbers.Length == 0)
            {
                return new { error = "No valid numbers provided" };
            }

            return new
            {
                count = numbers.Length,
                sum = numbers.Sum(),
                average = numbers.Average(),
                minimum = numbers.Min(),
                maximum = numbers.Max(),
                median = CalculateMedian(numbers)
            };
        }
        catch (Exception ex)
        {
            return new { error = $"Failed to parse numbers: {ex.Message}" };
        }
    }

    /// <summary>
    /// Demonstrates file system operations (reading files).
    /// </summary>
    /// <param name="filePath">Path to the file to read</param>
    /// <returns>File contents or error message</returns>
    [McpServerTool(Name = "read_text_file")]
    [Description("Reads the contents of a text file")]
    public static async Task<string> ReadTextFile(
        [Description("Path to the text file to read")] string filePath)
    {
        try
        {
            // Security check - only allow reading from current directory and subdirectories
            var fullPath = Path.GetFullPath(filePath);
            var currentDir = Directory.GetCurrentDirectory();

            if (!fullPath.StartsWith(currentDir))
            {
                return "Error: Can only read files from the current directory and its subdirectories";
            }

            if (!File.Exists(fullPath))
            {
                return $"Error: File not found: {filePath}";
            }

            var content = await File.ReadAllTextAsync(fullPath);
            return $"File content ({content.Length} characters):\n{content}";
        }
        catch (Exception ex)
        {
            return $"Error reading file: {ex.Message}";
        }
    }

    /// <summary>
    /// Helper method to calculate median.
    /// </summary>
    private static double CalculateMedian(double[] numbers)
    {
        var sorted = numbers.OrderBy(x => x).ToArray();
        var mid = sorted.Length / 2;

        return sorted.Length % 2 == 0
            ? (sorted[mid - 1] + sorted[mid]) / 2
            : sorted[mid];
    }
}
