# Icon Instructions for NuGet Package

## Requirements

- **Size**: Must be under 1MB
- **Format**: PNG or JPEG
- **Recommended dimensions**: 128x128 pixels
- **File name**: `icon.png`

## Current Status

The template package does not currently include an icon to ensure compatibility with NuGet 5.8 size limits.

## To Add an Icon

1. Create or obtain a 128x128 pixel PNG icon representing the MCP (Model Context Protocol) concept
2. Save it as `icon.png` in the root directory
3. Uncomment the icon line in the nuspec file:

   ```xml
   <icon>icon.png</icon>
   ```

4. Add the icon file to the files section:

   ```xml
   <file src="icon.png" target="" />
   ```

## Suggested Icon Concept

- Gear or network icon representing protocol/connectivity
- .NET branding colors (purple/blue)
- Simple, clear design that works at small sizes
- Professional appearance suitable for NuGet.org

## Alternative Options

- Use an external icon URL with `<iconUrl>` instead of embedded icon
- Create an SVG icon and convert to PNG at appropriate size

## NuGet Version Compatibility Note

The embedded icon feature requires NuGet 5.3+. For maximum compatibility, you can also add:

```xml
<iconUrl>https://your-domain.com/icon.png</iconUrl>
```
