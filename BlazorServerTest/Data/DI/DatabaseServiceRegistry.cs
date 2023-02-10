using BlazorServerTest.Data.Infrastructure;
using BlazorServerTest.Data.Repositories;
using BlazorServerTest.Data.Repositories.Interfaces;

namespace BlazorServerTest.Data.DI;
public static class DatabaseServiceRegistry
{
    public static void AddEntityFrameworkSetup(this IServiceCollection services)
    {
        services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IRecipeRepository, WeatherForecastRepository>();
    }
}
