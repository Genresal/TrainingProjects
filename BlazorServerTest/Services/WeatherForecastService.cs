using BlazorServerTest.Data.Entities;
using BlazorServerTest.Data.Repositories.Interfaces;

namespace BlazorServerTest.Services
{
    public class WeatherForecastService : BaseService<WeatherForecast>
    {
        public WeatherForecastService(IWeatherForecastRepository repository) : base(repository)
        {
        }

        public async Task<IEnumerable<WeatherForecast>> GetForecastAsync(string? search)
        {
	        var data = (await _repository.GetAll()).ToList();
	        if (!string.IsNullOrEmpty(search))
	        {
		        return data.Where(x => x.Summary is not null && x.Summary.ToUpper().Contains(search.ToUpper()));
			}

	        return data;
        }

        public Task<WeatherForecast> AddDefault()
        {
            return _repository.Add(new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Summary = "qwerty",
                TemperatureC = 24,
            });
        }
    }
}