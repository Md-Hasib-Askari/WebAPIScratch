using System.Reflection;

namespace WebAPIScratch.Core;

public class ControllerRegistrar
{
    public static void RegisterControllerRoutes(Router router, object controller)
    {
        Type type = controller.GetType();

        foreach (MethodInfo method in type.GetMethods())
        {
            var route = method.GetCustomAttribute<RouteAttribute>();
            if (route is null)
                continue;

            router.Map(
                route.Method,
                route.Path,
                (request, stream) =>
                {
                    method.Invoke(controller, [request, stream]);
                }
            );
        }
    }
}
