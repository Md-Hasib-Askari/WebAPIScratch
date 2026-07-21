using System.Net.Sockets;

namespace WebAPIScratch;

public class Router
{
    private readonly Dictionary<string, Action<HttpRequest, NetworkStream>> _routes = [];

    public void Map(string method, string path, Action<HttpRequest, NetworkStream> handler)
    {
        string key = $"{method}:{path}";
        _routes[key] = handler;
    }

    public void Dispatch(HttpRequest request, NetworkStream stream)
    {
        string key = $"{request.Method}:{request.Path}";

        if (_routes.TryGetValue(key, out var handler))
        {
            handler(request, stream);
        }
        else
        {
            HttpResponseWriter.WriteResponse(stream, 404, "Not Found", "No matching route.");
        }
    }
}
