using BlazorServerTest.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BlazorServerTest.Core.Data.Contexts;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
        //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    //SqlLite
    //In SqLite when i delete entities from result table count works from cache or smth, it uses onl data!!!
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*
        if (!optionsBuilder.IsConfigured)   // For inmemory testing
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "./Db.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
        */
        //optionsBuilder.UseInMemoryDatabase("MyInMemoryDatabase");
        optionsBuilder.EnableSensitiveDataLogging();
    }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<RecipeCategory> RecipeCategories { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Step> Steps { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
