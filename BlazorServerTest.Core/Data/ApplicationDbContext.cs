using BlazorServerTest.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTest.Core.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    //SqlLite
    //In SqLite when i delete entities from result table count works from cache or smth, it uses onl data!!!
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*if (!optionsBuilder.IsConfigured)   // For inmemory testing
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "./Db.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
        */
        optionsBuilder.UseInMemoryDatabase("MyInMemoryDatabase");
    }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<RecipeCategory> RecipeCategories { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Step> Steps { get; set; }

    //Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Recipe>()
            .HasMany(x => x.Categories)
            .WithMany(x => x.Recipes)
            .UsingEntity<RecipeCategory>(
                e => e
                    .HasOne(pt => pt.Category)
                    .WithMany(),
                e => e
                    .HasOne(x => x.Recipe)
                    .WithMany(),
                e =>
                {
                    e.HasKey(t => new { t.RecipeId, t.CategoryId });
                    e.ToTable("RecipeCategories");
                });
    }
}
