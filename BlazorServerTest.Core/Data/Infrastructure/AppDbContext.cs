using BlazorServerTest.Core.Data.Entities;
//using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTest.Core.Data.Infrastructure;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    /*  //SqlLite
     /* //In SqLite when i delete entities from result table count works from cache or smth, it uses onl data!!!
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)   // For inmemory testing
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "/Data/Db.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }*/

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<Category> Categories { get; set; }


    //Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Recipe>()
            .HasMany(c => c.Categories)
            .WithMany(s => s.Recipes)
            .UsingEntity<RecipeCategory>(
                j => j
                    .HasOne(pt => pt.Category)
                    .WithMany(t => t.RecipeCategories)
                    .HasForeignKey(pt => pt.CategoryId),
                j => j
                    .HasOne(pt => pt.Recipe)
                    .WithMany(p => p.RecipeCategories)
                    .HasForeignKey(pt => pt.RecipeId),
                j =>
                {
                    //j.Property(pt => pt.EnrollmentDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    //j.Property(pt => pt.Mark).HasDefaultValue(3);
                    j.HasKey(t => new { t.RecipeId, t.CategoryId });
                    j.ToTable("RecipeCategories");
                });
    }
}
