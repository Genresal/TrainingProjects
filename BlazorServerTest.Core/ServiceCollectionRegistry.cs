using BlazorServerTest.Core.AutoMapper;
using BlazorServerTest.Core.Data;
using BlazorServerTest.Core.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlazorServerTest.Core;
public static class ServiceCollectionRegistry
{
    private const string MssqlConnectionString = "Server=localhost;Database=BlazorTest;Trusted_Connection=True;Encrypt=False;";
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddEntityFrameworkSqlite().AddDbContext<ApplicationDbContext>();
        //services.AddDbContext<AppDbContext>(x => x.UseSqlServer(MssqlConnectionString));

        services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);

        services.AddTransient<RecipeRepository>();
        services.AddTransient<CategoryRepository>();

        //services.AddTransient<AuxService>();
        //services.AddHostedService<TimerHostedService>();
    }
}
