using BlazorServerTest.Services.Interfaces;

namespace BlazorServerTest.Services.DI;
public static class ServiceCollectionRegistry
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<WeatherForecastService>();
        services.AddTransient<IBackgroundService, BackgroundService>();
    }
}
