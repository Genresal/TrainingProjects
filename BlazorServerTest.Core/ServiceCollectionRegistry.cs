using BlazorServerTest.Core.BackgroundServices;
using BlazorServerTest.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorServerTest.Core;
public static class ServiceCollectionRegistry
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<RecipeService>();
        services.AddTransient<CategoryService>();

        services.AddTransient<AuxService>();
        services.AddHostedService<TimerHosterService>();
    }
}
