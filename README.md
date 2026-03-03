# thunderbird-pc-godot-dotnet

Godot .NET client scaffold with:
- online session gateway (gRPC)
- offline action journal and upload flow
- contract-driven message types

## Development

```bash
dotnet restore Thunderbird.Client.slnx
dotnet build Thunderbird.Client.slnx
```

Integrate with Godot by opening this folder as a C# Godot project.

## Private NuGet Feed

`nuget.config` reads credentials from:

- `NUGET_FEED_URL`
- `NUGET_FEED_USERNAME`
- `NUGET_FEED_TOKEN`
