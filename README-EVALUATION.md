# Port Support Evaluation - Summary

## Task Completed ✅

I have successfully evaluated whether the McpServerTemplate supports ports higher than 9000 and prepared all documentation for creating a bug issue.

---

## Findings

### Question: Does McpServerTemplate support ports higher than 9000?

**Answer: YES, but with a caveat**

1. ✅ **Manual specification works**: The template correctly accepts and uses ports above 9000 when users explicitly specify them via CLI parameters
   ```bash
   dotnet new mcp-server --kestrelHttpPort 10000 --kestrelHttpsPort 11000
   ```

2. ❌ **Auto-generation uses lower ports**: When no ports are specified, the template auto-generates ports in the 5000-7300 range (below 9000)

### The Bug

**Inconsistent port ranges:**
- Kestrel ports: 5000-7300 (below 9000) ❌
- AppHost ports: 15000-23300 (above 9000) ✅

This inconsistency can cause issues in environments where ports below 9000 require elevated privileges.

---

## What I've Done

### 1. Comprehensive Testing
- ✅ Built and packed the template
- ✅ Installed it locally
- ✅ Created test projects with custom ports (above 9000)
- ✅ Created test projects with default auto-generated ports
- ✅ Verified the generated launchSettings.json files
- ✅ Confirmed no existing GitHub issues about this topic

### 2. Documentation Created

**PORT-EVALUATION-REPORT.md**
- Complete technical analysis
- Port range details from template.json
- Test results and examples
- Recommended fix with code

**ISSUE-TEMPLATE.md**
- Ready-to-file GitHub issue
- Includes all necessary information:
  - Title: "McpServerTemplate auto-generates kestrel ports below 9000"
  - Label: bug
  - Current behavior with examples
  - Expected behavior with suggested fix
  - Testing evidence
  - Workaround instructions
  - Impact assessment

### 3. Files Committed
- PORT-EVALUATION-REPORT.md - Technical evaluation
- ISSUE-TEMPLATE.md - GitHub issue content
- README-EVALUATION.md - This summary (to be committed)

---

## What Needs to Be Done Next

### Create the GitHub Issue

**I was unable to create the GitHub issue automatically due to authentication limitations.**

You need to manually create the issue using the prepared template:

1. Go to: https://github.com/timheuer/dotnet-mcp-server/issues/new
2. Open the file: `ISSUE-TEMPLATE.md` in this repository
3. Copy the content (excluding the instruction section at the bottom)
4. Paste into the new issue form
5. Add the label: `bug`
6. Submit the issue

**Everything is ready** - just copy and paste from ISSUE-TEMPLATE.md!

---

## Technical Details

### The Problem (in template.json)

```json
"kestrelHttpPortGenerated": {
  "generator": "port",
  "parameters": {
    "low": 5000,    // ❌ Below 9000
    "high": 5300
  }
}
```

### The Fix

```json
"kestrelHttpPortGenerated": {
  "generator": "port",
  "parameters": {
    "low": 10000,   // ✅ Above 9000
    "high": 10300
  }
}
```

### Location
File: `src/.template.config/template.json`  
Lines to update: 
- kestrelHttpPortGenerated (lines ~265-267)
- kestrelHttpsPortGenerated (lines ~286-289)

---

## Conclusion

The McpServerTemplate **technically supports** ports above 9000 (no code restrictions exist), but the **default auto-generated port ranges need to be updated** to match the pattern used by AppHost ports (above 9000).

This is a **low-to-medium severity bug** with an easy workaround and a straightforward fix.

**Next action:** Create the GitHub issue using ISSUE-TEMPLATE.md ✅
