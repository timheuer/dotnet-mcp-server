# Port Support Evaluation for McpServerTemplate

**Date:** 2025-11-04  
**Evaluator:** GitHub Copilot Workspace Agent  
**Issue:** Evaluate if McpServerTemplate supports ports higher than 9000

---

## Executive Summary

The McpServerTemplate **DOES support** ports higher than 9000 when explicitly specified by users, but **auto-generates default ports BELOW 9000** for Kestrel endpoints (5000-7300 range). This creates an inconsistency with AppHost ports which are all generated above 9000 (15000-23300 range).

**Recommendation:** This should be filed as a bug to update the default port generation ranges for consistency.

---

## Detailed Findings

### 1. Template Configuration Analysis

Location: `src/.template.config/template.json`

#### Kestrel Port Ranges (McpServerTemplate project)
```json
"kestrelHttpPortGenerated": {
  "generator": "port",
  "parameters": {
    "low": 5000,
    "high": 5300
  }
}

"kestrelHttpsPortGenerated": {
  "generator": "port", 
  "parameters": {
    "low": 7000,
    "high": 7300
  }
}
```
❌ **Below 9000**

#### AppHost Port Ranges (when Aspire integration is enabled)
```json
"appHostHttpPortGenerated":        { "low": 15000, "high": 15300 }
"appHostHttpsPortGenerated":       { "low": 17000, "high": 17300 }
"appHostMcpHttpPortGenerated":     { "low": 18000, "high": 18300 }
"appHostOtlpHttpPortGenerated":    { "low": 19000, "high": 19300 }
"appHostResourceHttpPortGenerated":{ "low": 20000, "high": 20300 }
"appHostOtlpHttpsPortGenerated":   { "low": 21000, "high": 21300 }
"appHostResourceHttpsPortGenerated":{ "low": 22000, "high": 22300 }
"appHostMcpHttpsPortGenerated":    { "low": 23000, "high": 23300 }
```
✅ **All above 9000**

### 2. Practical Testing

#### Test 1: Auto-generated Ports (Default)
```bash
dotnet new mcp-server -n TestGenerated --TransportMethod streamable-http -f net9.0
```

**Result:** Generated `launchSettings.json` with:
- HTTP: `http://localhost:5058` (port from 5000-5300 range)
- HTTPS: `https://localhost:7126` (port from 7000-7300 range)

❌ **Both ports below 9000**

#### Test 2: Custom Ports Above 9000
```bash
dotnet new mcp-server -n TestDefault --TransportMethod streamable-http -f net9.0 \
  --kestrelHttpPort 10000 --kestrelHttpsPort 10001
```

**Result:** Generated `launchSettings.json` with:
- HTTP: `http://localhost:10000`
- HTTPS: `https://localhost:10001`

✅ **Template accepts and uses ports above 9000 correctly**

### 3. Port Parameter Support

The template provides CLI parameters for all port configurations:

```
-k, --kestrelHttpPort <kestrelHttpPort>      Port number for HTTP endpoint
-ke, --kestrelHttpsPort <kestrelHttpsPort>   Port number for HTTPS endpoint
-ap, --appHostHttpPort <appHostHttpPort>     Port number for AppHost HTTP
--appHostHttpsPort <appHostHttpsPort>        Port number for AppHost HTTPS
# ... and more
```

**Type:** `integer` (no validation or restrictions found)  
**Default:** `0` (triggers auto-generation)

---

## Identified Issue

### Bug: Inconsistent Port Range Defaults

**Severity:** Medium  
**Component:** Template Configuration (`src/.template.config/template.json`)

**Problem:**
- Kestrel ports default to 5000-7300 range (below 9000)
- AppHost ports default to 15000-23300 range (above 9000)
- This inconsistency can cause issues in restricted environments

**Why ports above 9000 matter:**
1. Ports below 1024 require elevated privileges on Unix systems
2. Ports 1024-9000 are commonly used and may have conflicts
3. Ports above 9000 are generally safer for development
4. Consistency across all template-generated ports improves UX

**Impact:**
- Default projects may fail in environments with port restrictions
- Inconsistent experience between Kestrel and AppHost configurations
- Users must manually override ports to match AppHost range

**Workaround:**
Users can specify ports manually when creating projects:
```bash
dotnet new mcp-server --kestrelHttpPort 10000 --kestrelHttpsPort 11000 ...
```

---

## Recommended Fix

Update `src/.template.config/template.json`:

```json
"kestrelHttpPortGenerated": {
  "type": "generated",
  "generator": "port",
  "parameters": {
    "low": 10000,  // Changed from 5000
    "high": 10300   // Changed from 5300
  }
}

"kestrelHttpsPortGenerated": {
  "type": "generated",
  "generator": "port",
  "parameters": {
    "low": 11000,  // Changed from 7000
    "high": 11300   // Changed from 7300
  }
}
```

This maintains separation from AppHost ports while staying above 9000.

---

## Next Action Required

**Create GitHub Issue with:**
- Title: "McpServerTemplate auto-generates kestrel ports below 9000"
- Label: `bug`
- Content: See `/tmp/issue-to-create.md` for full issue template

Note: Issue creation requires GitHub API access which is not available in this environment.
The issue content has been prepared and is ready to be filed manually.

---

## Files Examined

- `/home/runner/work/dotnet-mcp-server/dotnet-mcp-server/src/.template.config/template.json`
- `/home/runner/work/dotnet-mcp-server/dotnet-mcp-server/src/McpServerTemplate/Properties/launchSettings.json`
- `/home/runner/work/dotnet-mcp-server/dotnet-mcp-server/src/McpServerTemplate.AppHost/Properties/launchSettings.json`

## Test Projects Created

- `/tmp/test-default/TestDefault` - Custom ports above 9000 test
- `/tmp/test-generated/TestGenerated` - Auto-generated ports test

---

## Conclusion

The McpServerTemplate technically supports ports higher than 9000 (no code restrictions prevent their use), but the **default auto-generated port ranges for Kestrel are below 9000**, creating an inconsistency that should be fixed for better user experience and to avoid potential issues in restricted environments.

**Action Required:** File bug issue in GitHub repository.
