using BlazorServerTest.Data.Entities;
using BlazorServerTest.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace BlazorServerTest.BLL.Services
{
    public class RecipeService : BaseService<RecipeEntity>
    {
        private IRecipeRepository _recipeRepository;
        public RecipeService(IRecipeRepository repository) : base(repository)
        {
            _recipeRepository = repository;
        }

        public async Task<IEnumerable<RecipeEntity>> GetForecastAsync(string? search)
        {
            var data = (await _repository.GetAll()).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                return data.Where(x => x.Name is not null && x.Name.ToUpper().Contains(search.ToUpper()));
            }

            return data;
        }

        public Task<DtResponce<RecipeEntity>> LoadTable(DtParameters dtParameters)
        {
            //return _recipeRepository.LoadTable(dtParameters);

            var searchBy = dtParameters.Search?.Value;

            // if we have an empty search then just order the results by Id ascending
            var orderCriteria = "Id";
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }

            Expression<Func<RecipeEntity, bool>> searchByEx = default;

            if (!string.IsNullOrEmpty(searchBy))
            {
                searchByEx = r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper());
            }

            return _repository.LoadTable(dtParameters.Draw, dtParameters.Start, dtParameters.Length, orderCriteria, orderAscendingDirection, searchByEx);
        }

        public Task<RecipeEntity> AddDefault()
        {
            return _repository.Add(new RecipeEntity
            {
                Name = "DefaultRecipe"
            });
        }
    }
}