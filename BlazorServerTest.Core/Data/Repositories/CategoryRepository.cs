using AutoMapper;
using BlazorServerTest.Core.Data.Entities;

namespace BlazorServerTest.Core.Data.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        private readonly RecipeRepository _recipeRepository;

        public CategoryRepository(ApplicationDbContext context,
            RecipeRepository recipeRepository,
            IMapper mapper) : base(context, mapper)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<List<Category>> CalculateRecipesQuantity()
        {
            var categories = await PagedFindAsync<string>(1, 10);

            foreach (var cat in categories.Items)
            {
                var catRecipesCount = await _recipeRepository.CountAsync(x => x.Categories.Contains(cat));


                if (cat.Quantity != catRecipesCount)
                {
                    cat.Quantity = catRecipesCount;
                }
            }

            return categories.Items.ToList();
        }

        public Task<List<Category>> GetAllAsync()
        {
            return CalculateRecipesQuantity();
        }
    }
}
