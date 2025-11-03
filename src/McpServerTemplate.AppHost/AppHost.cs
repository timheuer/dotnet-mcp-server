var builder = DistributedApplication.CreateBuilder(args);

var server =
    builder.AddProject<Projects.McpServerTemplate>("mcp-server");

builder.AddMcpInspector("mcp-inspector")
    .WithMcpServer(server);

builder.Build().Run();
