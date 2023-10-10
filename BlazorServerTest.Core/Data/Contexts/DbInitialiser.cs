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
                        ImageUrl = "https://res.cloudinary.com/dx07nzr59/image/upload/v1695145914/none/fxbng5bf2ybezb3rwytl.jpg",
                        Created = DateTime.Now,
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
                        },
                        Steps = new List<Step>()
                        {
                            new Step() {Description = "Step 1", Order = 1},
                            new Step() {Description = "Step 2", Order = 2},
                            new Step() {Description = "Step 3", Order = 3},
                            new Step() {Description = "Decide where and how you want to store your decision documents. You can use a database, file storage, or any other suitable storage mechanism.", Order = 5},
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
