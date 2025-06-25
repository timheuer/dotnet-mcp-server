# MCP Server Template for .NET

[![NuGet](https://img.shields.io/nuget/v/Microsoft.McpServer.Csharp.svg)](https://www.nuget.org/packages/Microsoft.McpServer.Csharp/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Microsoft.McpServer.Csharp.svg)](https://www.nuget.org/packages/Microsoft.McpServer.Csharp/)

A comprehensive .NET project template for creating **Model Context Protocol (MCP) servers** using C# and the official ModelContextProtocol package.

## 🚀 Quick Start

### Install the Template

```bash
dotnet new install Microsoft.McpServer.Csharp
```

### Create Your MCP Server

**Standard (StdIO) MCP Server:**

```bash
dotnet new mcpserver -n MyMcpServer
cd MyMcpServer
dotnet build
dotnet run
```

**HTTP-based MCP Server:**

```bash
dotnet new mcpserver -n MyMcpServer --EnableHttpTransport true
cd MyMcpServer
dotnet build
dotnet run
```

## 📦 What's Included

- ✅ **Complete MCP Server Setup** with Microsoft.Extensions.Hosting
- ✅ **Sample Tools** demonstrating basic and advanced MCP capabilities
- ✅ **Prompt Templates** for AI interactions
- ✅ **Dependency Injection** support with examples
- ✅ **Async Operations** examples
- ✅ **Comprehensive Documentation** and usage examples
- ✅ **GitHub Actions** workflows for CI/CD
- ✅ **Security Best Practices** with file access restrictions

## 🛠 Template Features

### Transport Options

- **StdIO Transport** (default) - Standard input/output communication for desktop AI tools like GitHub Copilot and Claude Desktop
- **HTTP Transport** - Web-based API for cloud and web-based AI integrations, runs on port 5000 (HTTP) and 5001 (HTTPS) by default

**StdIO Transport** is ideal for:

- Local desktop AI clients (GitHub Copilot, Claude Desktop)
- Direct process communication
- Simple configuration without network setup

**HTTP Transport** is ideal for:

- Web-based AI applications
- Cloud deployments
- Multiple client access
- REST API integration

### Sample Tools Included

- **Echo & ReverseEcho** - Basic text manipulation
- **Math Operations** - Addition with parameter validation
- **DateTime** - Current timestamp in ISO format
- **Random Number** - Configurable range generation
- **File Operations** - Secure file reading with restrictions
- **Advanced Examples** - Dependency injection, async operations

### Sample Prompts

- **Content Summarization** - AI-ready summarization prompts
- **Code Analysis** - Multi-language code review prompts
- **Concept Explanation** - Audience-targeted explanations

## 🎯 Use Cases

Perfect for creating MCP servers that integrate with:

- **GitHub Copilot** in VS Code
- **Claude Desktop** applications
- **Custom AI agents** and chatbots
- **Enterprise AI workflows**

## 📚 Documentation

The generated project includes:

- Complete setup instructions
- Tool development examples
- Client configuration samples
- Best practices guide

## 🔗 Links

- [Model Context Protocol Documentation](https://modelcontextprotocol.io/)
- [MCP C# SDK Repository](https://github.com/modelcontextprotocol/csharp-sdk)
- [Microsoft .NET AI Documentation](https://learn.microsoft.com/en-us/dotnet/ai/get-started-mcp)

## 📄 License

MIT License - see the generated project for full details.
