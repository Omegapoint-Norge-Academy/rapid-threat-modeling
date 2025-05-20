using Rtm.BlazorClient.Services;

namespace Rtm.BlazorClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<HybridCacheService>();
    }
}