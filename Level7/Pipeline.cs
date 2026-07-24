using System.Net.Sockets;

namespace WebAPIScratch;

public delegate void RequestDelegate(HttpRequest request, NetworkStream stream);
public delegate RequestDelegate Middleware(RequestDelegate next);

public class Pipeline
{
    private readonly List<Middleware> _middlewares = [];

    public Pipeline Use(Middleware middleware)
    {
        _middlewares.Add(middleware);
        return this;
    }

    public RequestDelegate Build(RequestDelegate finalHandler)
    {
        RequestDelegate pipeline = finalHandler;

        for (int i = _middlewares.Count - 1; i >= 0; i--)
        {
            pipeline = _middlewares[i](pipeline);
        }

        return pipeline;
    }
}
