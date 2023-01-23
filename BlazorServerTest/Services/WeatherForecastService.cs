using BlasorServerTest.Repositories.Interfaces;
using BlazorServerTest.Data;

namespace BlazorServerTest.Services
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        IWeatherForecastRepository _repository;

        public WeatherForecastService(IWeatherForecastRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<WeatherForecast>> GetForecastAsync(DateOnly startDate)
        {
            return await _repository.GetAll();
        }

        public async Task<WeatherForecast> AddDefault()
        {
            return await _repository.Add(new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Summary = "qwerty",
                TemperatureC = 24,
            });
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        /*
        public Task<WeatherForecast[]> GetForecastAsync(DateOnly startDate)
        {
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray());
        }*/
    }
}