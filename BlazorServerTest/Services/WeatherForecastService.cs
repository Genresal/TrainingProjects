using BlazorServerTest.Data.Entities;
using BlazorServerTest.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace BlazorServerTest.Services
{
    public class WeatherForecastService : BaseService<WeatherForecast>
    {
        private IWeatherForecastRepository _weatherForecastRepository;
        public WeatherForecastService(IWeatherForecastRepository repository) : base(repository)
        {
            _weatherForecastRepository = repository;
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

        public Task<DtResponce<WeatherForecast>> LoadTable(DtParameters dtParameters)
        {
            //return _weatherForecastRepository.LoadTable(dtParameters);

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

            Expression<Func<WeatherForecast, bool>> searchByEx = default;

            if (!string.IsNullOrEmpty(searchBy))
            {
                searchByEx = r => r.Summary != null && r.Summary.ToUpper().Contains(searchBy.ToUpper());
            }

            return _repository.LoadTable(dtParameters.Draw, dtParameters.Start, dtParameters.Length, orderCriteria, orderAscendingDirection, searchByEx);
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