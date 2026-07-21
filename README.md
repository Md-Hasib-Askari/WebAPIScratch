# WebAPIScratch

Building a web API from scratch in C# without ASP.NET, progressing level by level. Each level builds on the previous one, starting from a bare TCP listener and evolving toward a fully featured HTTP API framework.

## How It Works

The project lives entirely inside `System.Net.Sockets` (raw TCP). No web frameworks, no middleware pipelines, no routing libraries. Every layer is hand-built: TCP acceptance, HTTP parsing, routing, serialization, and so on.

## Levels

| Level | Description |
| --- | --- |
| `Level1/` | Raw TCP server. Listens on port 5000, accepts one connection at a time, reads the raw bytes, prints them to the console, then closes. |
| `Level2/` | _(placeholder)_ |

As new levels are added, the README will be updated with what each level introduces.

## Running

```sh
dotnet run --project Level<N>
```

Replace `<N>` with the level number.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## License

See [LICENSE](LICENSE).
