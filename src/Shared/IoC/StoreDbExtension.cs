using Microsoft.Extensions.DependencyInjection;

namespace Shared.IoC;

public static class StoreDbExtension
{
    public static IServiceCollection 
        AddStoreDbAsSingleton(this IServiceCollection services)
    {
        services.AddSingleton<StoreDbFactory>();
        
        return services;
    }
}