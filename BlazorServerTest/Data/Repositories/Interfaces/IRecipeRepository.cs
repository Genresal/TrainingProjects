using BlazorServerTest.Data.Entities;

namespace BlazorServerTest.Data.Repositories.Interfaces;
public interface IRecipeRepository : IBaseRepository<RecipeEntity>
{
    Task<DtResponce<RecipeEntity>> LoadTable(DtParameters dtParameters);
}
