namespace WebAPIScratch;

public class ServiceContainer
{
    private readonly Dictionary<Type, Func<object>> _factories = [];

    // 1. Service Registration
    public void Register<TService>(Func<TService> factory)
    {
        _factories[typeof(TService)] = () =>
            factory() ?? throw new InvalidOperationException("Factory returns null value!");
    }

    // 2. Resolve
    public TService Resolve<TService>()
    {
        if (_factories.TryGetValue(typeof(TService), out var factory))
        {
            return (TService)factory();
        }

        throw new InvalidOperationException($"No registration found for {typeof(TService).Name}");
    }
}
