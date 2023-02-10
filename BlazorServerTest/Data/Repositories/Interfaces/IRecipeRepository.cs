using BlazorServerTest.Data.Entities;

namespace BlazorServerTest.Data.Repositories.Interfaces;
public interface IRecipeRepository : IBaseRepository<IngredientEntity>
{
    Task<DtResponce<IngredientEntity>> LoadTable(DtParameters dtParameters);
}
