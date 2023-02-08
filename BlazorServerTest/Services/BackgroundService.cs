using BlazorServerTest.Data;
using BlazorServerTest.Data.Entities;
using BlazorServerTest.Data.Repositories.Interfaces;
using BlazorServerTest.Services.Interfaces;
using Hangfire;
using Hangfire.Storage;

namespace BlazorServerTest.Services
{
    public class BackgroundService : IBackgroundService
    {
        IWeatherForecastRepository _repository;
        private readonly ILogger<BackgroundService> _logger;
        public BackgroundService(IWeatherForecastRepository repository, ILogger<BackgroundService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task GetAndSaveBackgroundAsync()
        {
            var data = new WeatherForecast
            {
                TemperatureC = 1,
                Summary = "Getted from background",
                Date = DateOnly.FromDateTime(DateTime.Now),
            };

            await Task.Delay(2 * 1000);

            await _repository.Add(data);
        }

        public async Task CheckAndMarkNewData()
        {
            var values = await _repository.GetAll();
            var newValues = values.Where(x => !x.IsChecked);
            foreach (var forecast in newValues)
            {
                forecast.IsChecked = true;
                await _repository.Update(forecast);
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
