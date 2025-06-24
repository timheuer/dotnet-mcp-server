# GitHub Actions Setup

This repository includes GitHub Actions workflows for automated building, testing, and publishing of the MCP Server template.

## Workflows

### 1. Build and Test (`build-and-test.yml`)
**Triggers**: Push to main/develop, Pull Requests
- ✅ Builds the template project
- ✅ Packs the template into a NuGet package
- ✅ Tests template installation and usage
- ✅ Uploads test artifacts

### 2. Pack and Publish (`pack-and-publish.yml`)
**Triggers**: Version tags (v1.0.0), Manual dispatch
- 📦 Packs the template with proper versioning
- 🚀 Publishes to NuGet.org
- 📋 Creates GitHub releases
- 🧪 Tests the published template

## Setup Instructions

### 1. Required Secrets
Add these secrets to your GitHub repository:

```
NUGET_API_KEY: Your NuGet.org API key
```

To get a NuGet API key:
1. Go to [nuget.org](https://www.nuget.org/)
2. Sign in and go to "API Keys"
3. Create a new API key with "Push new packages and package versions" scope
4. Add it as `NUGET_API_KEY` in GitHub repository secrets

### 2. Publishing a Release

#### Option A: Git Tags (Recommended)
```bash
# Create and push a version tag
git tag v1.0.0
git push origin v1.0.0
```

#### Option B: Manual Workflow
1. Go to Actions tab in GitHub
2. Select "Pack and Publish MCP Server Template"
3. Click "Run workflow"
4. Enter version and prerelease settings

### 3. Version Naming
- **Stable**: `v1.0.0` → `1.0.0`
- **Prerelease**: `v1.0.0-preview` → `1.0.0-preview`
- **Auto-preview**: `v1.0.0` + prerelease=true → `1.0.0-preview`

## Workflow Features

### ✅ Automatic Version Detection
- Extracts version from Git tags
- Supports manual version input
- Handles prerelease versioning

### 🧪 Template Testing
- Installs template after packaging
- Creates test project from template
- Builds test project to verify functionality

### 📦 Artifact Management
- Uploads NuGet packages as artifacts
- Retains packages for investigation
- Creates GitHub releases with usage instructions

### 🔒 Security
- Uses official GitHub actions
- Secure secret handling
- No secrets in logs

## Troubleshooting

### Build Failures
- Check .NET version compatibility
- Verify all files are included in repository
- Review build logs in Actions tab

### Publishing Failures
- Verify NUGET_API_KEY secret is set
- Check if package version already exists
- Review NuGet.org package validation rules

### Template Test Failures
- Ensure template.json is valid
- Check all template files are present
- Verify generated project builds correctly

## Local Testing

Test the workflows locally before pushing:

```bash
# Build and pack locally
dotnet build --configuration Release
nuget pack Microsoft.DotNet.MCP.Server.Template.nuspec

# Test template installation
dotnet new install ./Microsoft.DotNet.MCP.Server.Template.1.0.0-preview.nupkg
dotnet new mcpserver -n LocalTest
cd LocalTest && dotnet build
```
