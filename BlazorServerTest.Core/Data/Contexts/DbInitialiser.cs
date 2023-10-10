using BlazorServerTest.Core.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorServerTest.Core.Data.Contexts;

public static class DbInitializer
{
    public static async Task InitializeDb(this IServiceProvider provider)
    {
        try
        {
            using var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.Categories.Any())
            {
                List<Category> data = new();

                for (int i = 0; i < 8; i++)
                {
                    data.Add(new() { Name = Guid.NewGuid().ToString().Substring(0, 16) });
                }

                data[0].RecipeCategories = new List<RecipeCategory>()
            {
                new()
                {
                    Recipe = new Recipe()
                    {
                        Name = Guid.NewGuid().ToString().Substring(0, 16),
                        Marks = new List<RecipeMark>()
                        {
                            new() {Rating = 5},
                            new() {Rating = 5},
                            new() {Rating = 5},
                            new() {Rating = 4},
                        },
                        AverageRating = 4.75,
                        RecipeIngredients = new List<RecipeIngredient>()
                        {
                            new RecipeIngredient()
                            {
                                Quantity = 10,
                                Ingredient = new ()
                                {
                                    Name = "INGREDIENT!!!",
                                    Unit = "g."
                                }
                            }
                        }
                    },
                }
            };

                context.Categories.AddRange(data);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            //logger.LogError(e.HResult, "An exception occurred during run initialization of the data");
            //throw;
        }
    }
}
