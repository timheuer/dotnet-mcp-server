using System.ComponentModel;
using ModelContextProtocol.Server;

namespace McpServerTemplate.Tools;

/// <summary>
/// Sample MCP tools for demonstration purposes.
/// These tools can be invoked by MCP clients to perform various operations.
/// </summary>
[McpServerToolType]
public static class SampleTools
{
    /// <summary>
    /// Echoes the provided text back to the client.
    /// </summary>
    /// <param name="text">The text to echo</param>
    /// <returns>The echoed text</returns>
    [McpServerTool(Name = "echo")]
    [Description("Echoes the provided text back to the client")]
    public static string Echo(
        [Description("The text to echo")] string text)
    {
        return $"Echo: {text}";
    }

    /// <summary>
    /// Reverses the provided text and echoes it back to the client.
    /// </summary>
    /// <param name="text">The text to reverse and echo</param>
    /// <returns>The reversed text</returns>
    [McpServerTool(Name = "reverse_echo")]
    [Description("Reverses the provided text and echoes it back to the client")]
    public static string ReverseEcho(
        [Description("The text to reverse and echo")] string text)
    {
        var reversed = new string(text.Reverse().ToArray());
        return reversed;
    }

    /// <summary>
    /// Adds two numbers together and returns the sum.
    /// </summary>
    /// <param name="a">First number</param>
    /// <param name="b">Second number</param>
    /// <returns>The sum of the two numbers</returns>
    [McpServerTool(Name = "add_numbers")]
    [Description("Adds two numbers together and returns the sum")]
    public static int Add(
        [Description("First number")] int a,
        [Description("Second number")] int b)
    {
        return a + b;
    }

    /// <summary>
    /// Gets the current date and time in ISO 8601 format.
    /// </summary>
    /// <returns>Current date and time as ISO 8601 string</returns>
    [McpServerTool(Name = "get_current_datetime")]
    [Description("Gets the current date and time in ISO 8601 format")]
    public static string GetCurrentDateTime()
    {
        return DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
    }

    /// <summary>
    /// Generates a random number between the specified minimum and maximum values.
    /// </summary>
    /// <param name="min">Minimum value (inclusive)</param>
    /// <param name="max">Maximum value (exclusive)</param>
    /// <returns>A random number between min and max</returns>
    [McpServerTool(Name = "generate_random_number")]
    [Description("Generates a random number between the specified minimum and maximum values")]
    public static int GenerateRandomNumber(
        [Description("Minimum value (inclusive)")] int min = 0,
        [Description("Maximum value (exclusive)")] int max = 100)
    {
        var random = new Random();
        return random.Next(min, max);
    }
}
