# GitHub Issue Template

**This issue should be created in the repository: timheuer/dotnet-mcp-server**

---

## Title
McpServerTemplate auto-generates kestrel ports below 9000

## Labels
- `bug`

## Description

The McpServerTemplate currently auto-generates default port ranges for Kestrel HTTP/HTTPS endpoints that are below 9000, while all AppHost-related ports are configured to be above 9000. This inconsistency may cause issues in environments where ports below 9000 require elevated privileges or have other restrictions.

## Current Behavior

When creating a new MCP server project using the template without specifying custom ports:

```bash
dotnet new mcp-server -n MyServer --TransportMethod streamable-http -f net9.0
```

The generated `launchSettings.json` contains ports in these ranges:

### Kestrel (McpServerTemplate project)
- `kestrelHttpPort`: **5000-5300** ❌ (below 9000)
- `kestrelHttpsPort`: **7000-7300** ❌ (below 9000)

Example generated output:
```json
{
  "profiles": {
    "http": {
      "applicationUrl": "http://localhost:5058"
    },
    "https": {
      "applicationUrl": "https://localhost:7126;http://localhost:5058"
    }
  }
}
```

### AppHost (when Aspire is enabled)
- `appHostHttpPort`: **15000-15300** ✅ (above 9000)
- `appHostHttpsPort`: **17000-17300** ✅ (above 9000)
- `appHostMcpHttpPort`: **18000-18300** ✅ (above 9000)
- `appHostOtlpHttpPort`: **19000-19300** ✅ (above 9000)
- `appHostResourceHttpPort`: **20000-20300** ✅ (above 9000)
- All other AppHost ports: **21000-23300** ✅ (above 9000)

## Root Cause

The port ranges are defined in `src/.template.config/template.json`:

```json
"kestrelHttpPortGenerated": {
  "type": "generated",
  "generator": "port",
  "parameters": {
    "low": 5000,    // ❌ Should be above 9000
    "high": 5300
  }
},
"kestrelHttpsPortGenerated": {
  "type": "generated",
  "generator": "port",
  "parameters": {
    "low": 7000,    // ❌ Should be above 9000
    "high": 7300
  }
}
```

## Expected Behavior

For consistency with AppHost port ranges and to avoid potential privilege issues, the Kestrel port ranges should also be above 9000.

**Suggested fix:**

```json
"kestrelHttpPortGenerated": {
  "type": "generated",
  "generator": "port",
  "parameters": {
    "low": 10000,   // ✅ Above 9000
    "high": 10300
  }
},
"kestrelHttpsPortGenerated": {
  "type": "generated",
  "generator": "port",
  "parameters": {
    "low": 11000,   // ✅ Above 9000
    "high": 11300
  }
}
```

This maintains separation from AppHost ports while staying above 9000.

## Workaround

Users can manually specify ports above 9000 when creating a project:

```bash
dotnet new mcp-server -n MyServer --TransportMethod streamable-http \
  --kestrelHttpPort 10000 --kestrelHttpsPort 11000
```

The template correctly accepts and uses these custom port values.

## Impact

- **Medium severity**: Default generated projects may fail to run in restricted environments where ports below 9000 require privileges
- **UX inconsistency**: Kestrel and AppHost port ranges follow different patterns
- **Easy workaround**: Users can specify custom ports via CLI parameters

## Testing Evidence

✅ **Template accepts ports above 9000 when specified:**
```bash
$ dotnet new mcp-server -n Test --TransportMethod streamable-http \
    --kestrelHttpPort 10000 --kestrelHttpsPort 10001 -f net9.0

# Generated launchSettings.json:
"applicationUrl": "https://localhost:10001;http://localhost:10000"
```

❌ **Template generates ports below 9000 by default:**
```bash
$ dotnet new mcp-server -n Test --TransportMethod streamable-http -f net9.0

# Generated launchSettings.json:
"applicationUrl": "https://localhost:7126;http://localhost:5058"
```

## Additional Context

- This was discovered during an evaluation of port support in the McpServerTemplate
- The template itself has no code-level restrictions on port values
- All port parameters accept integers without validation limits
- Only the default auto-generation ranges need adjustment

## Files to Modify

1. `src/.template.config/template.json` - Update `kestrelHttpPortGenerated` and `kestrelHttpsPortGenerated` parameters

---

**To create this issue:**
1. Go to https://github.com/timheuer/dotnet-mcp-server/issues/new
2. Copy the content above (excluding this instruction section)
3. Add the `bug` label
4. Submit the issue
