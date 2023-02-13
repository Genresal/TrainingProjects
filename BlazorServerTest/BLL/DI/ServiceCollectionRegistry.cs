using BlazorServerTest.BLL.BackgroundServices;
using BlazorServerTest.BLL.Services;
using BlazorServerTest.BLL.Services.Interfaces;

namespace BlazorServerTest.BLL.DI;
public static class ServiceCollectionRegistry
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<RecipeService>();
        services.AddTransient<ICategoryService, CategoryService>();

        services.AddTransient<IBackgroundService, Services.BackgroundService>();
        services.AddHostedService<TimerHosterService>();
    }
}
