# MCP Server Template Development Guide

## Core Commands

- **Build**: `dotnet build` or `dotnet build Microsoft.McpServer.Template.csproj`
- **Pack template**: `dotnet pack Microsoft.McpServer.Template.csproj`
- **Uninstall previous template**: `dotnet new uninstall Microsoft.McpServer.Csharp`
- **Test template locally**: `dotnet new install ./artifacts/Microsoft.McpServer.Csharp.1.0.0-preview.nupkg`
- **Create from template**: `dotnet new mcpserver -n MyMcpServer [--EnableHttpTransport true]`

## Architecture Overview

This is a **dotnet template project** that generates MCP (Model Context Protocol) servers, not a runnable application itself:

- **Template source**: `src/` contains the actual template files with conditional compilation directives
- **Template config**: `.template.config/` defines parameters, conditional content, and CLI options
- **Package project**: `Microsoft.McpServer.Template.csproj` packages the template for NuGet distribution
- **Two transport modes**: StdIO (default, for desktop AI clients) vs HTTP (for web/cloud integration)

### Key Template Features

- **Conditional compilation** using `#if` directives for different transport modes
- **Central package management** via `Directory.Packages.props`
- **Attribute-based tool/prompt registration** using `[McpServerTool]` and `[McpServerPrompt]`
- **Dependency injection** pattern with `Microsoft.Extensions.Hosting`

## Template-Specific Patterns

### Tool Registration

```csharp
[McpServerToolType]
public static class SampleTools
{
    [McpServerTool(Name = "tool_name")]
    [Description("Tool description")]
    public static ReturnType MethodName([Description("param desc")] ParamType param) { }
}
```

### Prompt Registration

```csharp
[McpServerPromptType]
public static class SamplePrompts
{
    [McpServerPrompt(Name = "prompt_name")]
    [Description("Prompt description")]
    public static ChatMessage MethodName([Description("param desc")] string param) { }
}
```

### Conditional Template Content

- Use `#if (EnableHttpTransport)` for HTTP-specific code
- Use `#if (EnableOpenTelemetry)` for observability features
- Template parameters defined in `.template.config/template.json`

## Development Workflow

1. **Modify template files** in `src/` directory
2. **Test changes**: Pack template → Install locally → Generate test project → Build test project
3. **CI validates** template through GitHub Actions that test installation and project generation
4. **Publishing** handled via GitHub Actions with manual workflow dispatch

## Repository Structure

- `src/` - Template source code (actual MCP server implementation)
- `Microsoft.McpServer.Template.csproj` - Template packaging project
- `.github/workflows/` - CI/CD for template testing and publishing
- `artifacts/` - Generated NuGet packages
- `PACKAGING.md` - Template distribution instructions

## Template Testing Strategy

The CI workflow validates templates by:

1. Packing the template into a NuGet package
2. Installing the template locally using `dotnet new install`
3. Generating a test project with `dotnet new mcpserver`
4. Building the generated project to ensure it compiles

Always test template changes using this same pattern locally before committing.
