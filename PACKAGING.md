# Packaging Instructions for MCP Server Template

This document explains how to package and distribute this MCP Server template.

## Creating a Template Package

### Option 1: Local Template Installation

To install this template locally for development and testing:

1. **Navigate to the template directory**:
   ```bash
   cd path/to/dotnet-mcp-server
   ```

2. **Install the template locally**:
   ```bash
   dotnet new install .
   ```

3. **Verify the template is installed**:
   ```bash
   dotnet new list
   ```
   You should see `mcpserver` in the list of available templates.

4. **Create a new project using the template**:
   ```bash
   dotnet new mcpserver -n MyMcpServer
   ```

### Option 2: NuGet Package Distribution

To distribute this template via NuGet:

1. **Pack the template**:
   Or manually:
   ```bash
   dotnet pack Microsoft.McpServer.Template.csproj
   ```

3. **Install from the package**:
   ```bash
   dotnet new install Microsoft.McpServer.Csharp.1.0.0-preview.nupkg
   ```

### Option 3: GitHub Repository Template

To make this a GitHub template repository:

1. Go to your GitHub repository settings
2. Check "Template repository" in the repository settings
3. Users can then use the "Use this template" button to create new repositories

## Template Customization

### Template Parameters

You can add more template parameters by modifying the `.template.config/template.json` file:

```json
{
  "symbols": {
    "ServerName": {
      "type": "parameter",
      "description": "The name of the MCP server",
      "datatype": "string",
      "defaultValue": "MyMcpServer",
      "replaces": "McpServerTemplate"
    },
    "Port": {
      "type": "parameter",
      "description": "The port for HTTP transport (if used)",
      "datatype": "integer",
      "defaultValue": "3001"
    }
  }
}
```

### Conditional Content

You can make parts of the template conditional:

```json
{
  "symbols": {
    "IncludeAdvancedTools": {
      "type": "parameter",
      "datatype": "bool",
      "defaultValue": "true",
      "description": "Include advanced tool examples"
    }
  },
  "sources": [
    {
      "modifiers": [
        {
          "condition": "(!IncludeAdvancedTools)",
          "exclude": ["Tools/AdvancedTools.cs"]
        }
      ]
    }
  ]
}
```

## Testing the Template

1. **Create a test project**:
   ```bash
   dotnet new mcpserver -n TestMcpServer
   cd TestMcpServer
   ```

2. **Build the project**:
   ```bash
   dotnet build
   ```

3. **Run the server**:
   ```bash
   dotnet run
   ```

4. **Test with an MCP client** (such as the Claude desktop app or VS Code with GitHub Copilot)

## Uninstalling the Template

To remove the template:

```bash
dotnet new uninstall .
```

Or if installed via NuGet:

```bash
dotnet new uninstall Microsoft.McpServer.Csharp
```

## Best Practices

1. **Keep dependencies minimal**: Only include essential packages in the template
2. **Provide good examples**: Include both simple and advanced examples
3. **Document everything**: Ensure all code is well-documented
4. **Version appropriately**: Use semantic versioning for template releases
5. **Test thoroughly**: Test the template on multiple platforms and scenarios
