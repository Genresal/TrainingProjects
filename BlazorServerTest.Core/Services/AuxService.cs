using BlazorServerTest.Core.Data;
using BlazorServerTest.Core.Data.Entities;
using Hangfire;
using Hangfire.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlazorServerTest.Core.Services
{
    public class AuxService
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<Recipe> _dbSet;
        private readonly ILogger<AuxService> _logger;
        public AuxService(ApplicationDbContext context, ILogger<AuxService> logger)
        {
            _context = context;
            _dbSet = _context.Set<Recipe>();
            _logger = logger;
        }

        public async Task GetAndSaveBackgroundAsync()
        {
            var data = new Recipe
            {
                Name = "Getted from background",
            };

            await Task.Delay(2 * 1000);

            await _dbSet.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task CheckAndMarkNewData()
        {
            var values = await _dbSet.ToListAsync();
            var newValues = values.Where(x => string.IsNullOrEmpty(x.Description));
            foreach (var recipe in newValues)
            {
                recipe.Description = "This is not a drill, this is auto-generated recipe description";
                _dbSet.Update(recipe);
            }
        }

        public async Task JobImitation()
        {
            _logger.LogInformation("Job imitation fired");
        }

        public async Task AddJobs()
        {
            RecurringJob.AddOrUpdate<AuxService>(x => x.CheckAndMarkNewData(), Cron.Minutely);
        }

        public async Task RemoveJobs()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
        }
    }
}
