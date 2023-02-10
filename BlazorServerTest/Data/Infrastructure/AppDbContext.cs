using BlazorServerTest.Data.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTest.Data.Infrastructure;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "Db.db" };
        var connectionString = connectionStringBuilder.ToString();
        var connection = new SqliteConnection(connectionString);

        optionsBuilder.UseSqlite(connection);
    }

    public DbSet<RecipeEntity> Recipes { get; set; }
    public DbSet<IngredientEntity> Ingredients { get; set; }
    public DbSet<StepEntity> Steps { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }

    //Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeEntity>()
            .HasMany(x => x.Ingredients)
            .WithOne(x => x.Recipe)
            .HasForeignKey(x => x.RecipeId);

        modelBuilder.Entity<RecipeEntity>()
            .HasMany(x => x.Steps)
            .WithOne(x => x.Recipe)
            .HasForeignKey(x => x.RecipeId);

        modelBuilder.Entity<RecipeEntity>()
            .HasMany(x => x.Categories)
            .WithOne(x => x.Recipe)
            .HasForeignKey(x => x.RecipeId);
    }
}
