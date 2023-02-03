using BlasorServerTest.Repositories.Interfaces;
using BlazorServerTest.Data;
using BlazorServerTest.Services.Interfaces;

namespace BlazorServerTest.Services
{
    public class BackgroundService : IBackgroundService
    {
        IWeatherForecastRepository _repository;
        public BackgroundService(IWeatherForecastRepository repository)
        {
            _repository = repository;
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

            _repository.Add(data);
        }
    }
}
