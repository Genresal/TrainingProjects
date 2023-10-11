using BlazorServerTest.Core.Business;
using BlazorServerTest.Core.Business.AutoMapper;
using BlazorServerTest.Core.Data.Contexts;
using BlazorServerTest.Core.Data.Repositories.Categories;
using BlazorServerTest.Core.Data.Repositories.Ingredients;
using BlazorServerTest.Core.Data.Repositories.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlazorServerTest.Core;
public static class ServiceCollectionRegistry
{
    private const string MssqlConnectionString = "Server=localhost;Database=BlazorTest;Trusted_Connection=True;Encrypt=False;";
    public static void AddCoreServices(this IServiceCollection services)
    {
        /*
        services.AddEntityFrameworkSqlite().AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "./Db.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            options.UseSqlite(connection);

        }, ServiceLifetime.Transient);*/

        //services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(MssqlConnectionString));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase(databaseName: "InMemoryDb");
        }, ServiceLifetime.Transient);

        // Add Automapper
        services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);

        services.AddTransient<RecipeRepository>();
        services.AddTransient<CategoryRepository>();
        services.AddTransient<RecipeCategoryRepository>();
        services.AddTransient<RatingRepository>();
        services.AddTransient<RecipeIngredientRepository>();
        services.AddTransient<IngredientRepository>();

        services.AddTransient<CategoryManager>();
        services.AddTransient<RecipeManager>();
        services.AddTransient<RecipeRatingManager>();

        //services.AddTransient<AuxService>();
        //services.AddHostedService<TimerHostedService>();
    }
}
