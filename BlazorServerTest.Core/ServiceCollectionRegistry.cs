namespace BlazorServerTest.Core;
public static class ServiceCollectionRegistry
{
    private const string MssqlConnectionString = "Server=localhost;Database=BlazorTest;Trusted_Connection=True;Encrypt=False;";
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddEntityFrameworkSqlite().AddDbContext<ApplicationDbContext>();
        //services.AddDbContext<AppDbContext>(x => x.UseSqlServer(MssqlConnectionString));
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase(databaseName: "InMemoryDb");
        }, ServiceLifetime.Transient);

        services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);

        services.AddTransient<RecipeRepository>();
        services.AddTransient<CategoryRepository>();

        services.AddTransient<CategoryManager>();

        //services.AddTransient<AuxService>();
        //services.AddHostedService<TimerHostedService>();
    }
}
