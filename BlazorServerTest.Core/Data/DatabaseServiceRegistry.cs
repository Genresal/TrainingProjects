using BlazorServerTest.Core.Data.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorServerTest.Core.Data;
public static class DatabaseServiceRegistry
{
    public static void AddEntityFrameworkSetup(this IServiceCollection services)
    {
        services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>();
    }
}
