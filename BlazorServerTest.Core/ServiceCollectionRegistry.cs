using BlazorServerTest.Core.BackgroundServices;
using BlazorServerTest.Core.Data.Infrastructure;
using BlazorServerTest.Core.Data.Repositories;
using BlazorServerTest.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorServerTest.Core;
public static class ServiceCollectionRegistry
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>();

        services.AddTransient<RecipeRepository>();
        services.AddTransient<CategoryRepository>();

        services.AddTransient<AuxService>();
        services.AddHostedService<TimerHostedService>();
    }
}
