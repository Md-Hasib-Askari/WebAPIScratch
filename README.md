# WebAPIScratch

Building a web API from scratch in C# without ASP.NET, progressing level by level. Each level builds on the previous one, starting from a bare TCP listener and evolving toward a fully featured HTTP API framework.

## How It Works

The project lives entirely inside `System.Net.Sockets` (raw TCP). No web frameworks, no off-the-shelf middleware, no routing libraries. Every layer is hand-built: TCP acceptance, HTTP parsing, routing, middleware pipeline, and so on.

## Levels

| Level | Description |
| --- | --- |
| `Level1/` | Raw TCP server. Listens on port 5000, accepts one connection at a time, reads the raw bytes, prints them to the console, then closes. |
| `Level2/` | Adds an `HttpRequestReader` that parses raw HTTP into a structured `HttpRequest` record (method, path, version, headers, body). The server now prints a formatted request instead of raw bytes. |
| `Level3/` | Adds an `HttpResponseWriter` that sends a proper HTTP response (status line, headers, body) back to the client. The server now reads a request and replies with "Hello, world!". |
| `Level4/` | Adds a `Router` class that maps method+path pairs to handler functions. The server delegates to the router, which dispatches to the matching handler or returns a 404. |
| `Level5/` | Adds a `Pipeline` class that implements a middleware chain (Chain of Responsibility pattern). `RequestDelegate` and `Middleware` delegates enable components like logging and auth to wrap the router. `TcpServer.Start` now accepts a `RequestDelegate` instead of calling the router directly. |
| `Level6/` | Introduces a hand-rolled DI container (`ServiceContainer`), domain model (`AppUser`), repository interface (`IUserRepository`), and a concrete `UserRepository`. The `/users` endpoint resolves the repository from the container to serve real user data. |

## Running

```sh
dotnet run --project Level<N>
```

Replace `<N>` with the level number.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## License

See [LICENSE](LICENSE).
