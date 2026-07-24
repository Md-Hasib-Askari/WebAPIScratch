namespace WebAPIScratch.Core;

[AttributeUsage(AttributeTargets.Method)]
public class RouteAttribute(string method, string path) : Attribute
{
    public string Method { get; } = method;
    public string Path { get; } = path;
}
