using BlazorServerTest.Core.BackgroundServices;
using BlazorServerTest.Core.Data.Infrastructure;
using BlazorServerTest.Core.Data.Repositories;
using BlazorServerTest.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorServerTest.Core;
public static class ServiceCollectionRegistry
{
    private const string MssqlConnectionString = "Server=localhost;Database=BlazorTest;Trusted_Connection=True;Encrypt=False;";
    public static void AddCoreServices(this IServiceCollection services)
    {
        //services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>();
        services.AddDbContext<AppDbContext>(x => x.UseSqlServer(MssqlConnectionString));

        services.AddTransient<RecipeRepository>();
        services.AddTransient<CategoryRepository>();

        services.AddTransient<AuxService>();
        services.AddHostedService<TimerHostedService>();
    }
}
