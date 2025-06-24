using System.ComponentModel;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;

namespace McpServerTemplate.Prompts;

/// <summary>
/// Sample MCP prompts for demonstration purposes.
/// These prompts can be used by MCP clients to generate context-aware messages.
/// </summary>
[McpServerPromptType]
public static class SamplePrompts
{
    /// <summary>
    /// Creates a prompt to summarize the provided content.
    /// </summary>
    /// <param name="content">The content to summarize</param>
    /// <returns>A chat message requesting summarization</returns>
    [McpServerPrompt(Name = "summarize_content")]
    [Description("Creates a prompt to summarize the provided content")]
    public static ChatMessage Summarize(
        [Description("The content to summarize")] string content)
    {
        return new ChatMessage(
            ChatRole.User,
            $"Please summarize this content into a single sentence: {content}");
    }

    /// <summary>
    /// Creates a prompt to analyze code and suggest improvements.
    /// </summary>
    /// <param name="code">The code to analyze</param>
    /// <param name="language">The programming language</param>
    /// <returns>A chat message requesting code analysis</returns>
    [McpServerPrompt(Name = "analyze_code")]
    [Description("Creates a prompt to analyze code and suggest improvements")]
    public static ChatMessage AnalyzeCode(
        [Description("The code to analyze")] string code,
        [Description("The programming language")] string language = "C#")
    {
        return new ChatMessage(
            ChatRole.User,
            $"Please analyze this {language} code and suggest improvements:\n\n```{language.ToLower()}\n{code}\n```");
    }

    /// <summary>
    /// Creates a prompt to explain a technical concept.
    /// </summary>
    /// <param name="concept">The concept to explain</param>
    /// <param name="audienceLevel">The target audience level</param>
    /// <returns>A chat message requesting explanation</returns>
    [McpServerPrompt(Name = "explain_concept")]
    [Description("Creates a prompt to explain a technical concept")]
    public static ChatMessage ExplainConcept(
        [Description("The concept to explain")] string concept,
        [Description("The target audience level (beginner, intermediate, advanced)")] string audienceLevel = "intermediate")
    {
        return new ChatMessage(
            ChatRole.User,
            $"Please explain the concept of '{concept}' for a {audienceLevel} level audience. " +
            "Use clear examples and avoid unnecessary jargon.");
    }
}
