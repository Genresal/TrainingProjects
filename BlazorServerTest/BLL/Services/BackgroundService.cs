using BlazorServerTest.BLL.Services.Interfaces;
using BlazorServerTest.Data.Entities;
using BlazorServerTest.Data.Repositories.Interfaces;
using Hangfire;
using Hangfire.Storage;

namespace BlazorServerTest.BLL.Services
{
    public class BackgroundService : IBackgroundService
    {
        IRecipeRepository _repository;
        private readonly ILogger<BackgroundService> _logger;
        public BackgroundService(IRecipeRepository repository, ILogger<BackgroundService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task GetAndSaveBackgroundAsync()
        {
            var data = new RecipeEntity
            {
                Name = "Getted from background",
            };

            await Task.Delay(2 * 1000);

            await _repository.Add(data);
        }

        public async Task CheckAndMarkNewData()
        {
            var values = await _repository.GetAll();
            var newValues = values.Where(x => string.IsNullOrEmpty(x.Description));
            foreach (var recipe in newValues)
            {
                recipe.Description = "This is not a drill, this is auto-generated recipe description";
                await _repository.Update(recipe);
            }
        }

        public async Task JobImitation()
        {
            _logger.LogInformation("Job imitation fired");
        }

        public async Task AddJobs()
        {
            RecurringJob.AddOrUpdate<IBackgroundService>(x => x.CheckAndMarkNewData(), Cron.Minutely);
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
