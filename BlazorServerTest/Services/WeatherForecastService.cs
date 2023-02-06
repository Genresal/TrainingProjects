using BlazorServerTest.Data;
using BlazorServerTest.Data.Repositories.Interfaces;

namespace BlazorServerTest.Services
{
    public class WeatherForecastService
    {
        IWeatherForecastRepository _repository;

        public WeatherForecastService(IWeatherForecastRepository repository)
        {
            _repository = repository;
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

        public async Task<WeatherForecast> Add(WeatherForecast model)
        {
	        return await _repository.Add(model);
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
    }
}