using BlazorServerTest.Data.Entities;
using BlazorServerTest.Data.Extensions;
using BlazorServerTest.Data.Infrastructure;
using BlazorServerTest.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTest.Data.Repositories
{
    public class RecipeRepository : BaseRepository<IngredientEntity>, IRecipeRepository
    {
        private readonly ILogger<IRecipeRepository> _logger;

        public RecipeRepository(AppDbContext context, ILogger<IRecipeRepository> logger) : base(context)
        {
            _logger = logger;
        }

        // No obviously reasons to override basic method by it only for showing a different approach.
        public override Task<IngredientEntity> Update(IngredientEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            _logger.LogInformation($"Updated entity {entity}");

            return _context.SaveChangesAsync().ContinueWith(x => entity);
        }

        public async Task<DtResponce<IngredientEntity>> LoadTable(DtParameters dtParameters)
        {
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

            var result = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
            }

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = await result.CountAsync();
            var totalResultsCount = await _dbSet.CountAsync();

            return new DtResponce<IngredientEntity>()
            {
                Draw = dtParameters.Draw,
                RecordsTotal = totalResultsCount,
                RecordsFiltered = filteredResultsCount,
                Data = await result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToListAsync()
            };
        }
    }
}
