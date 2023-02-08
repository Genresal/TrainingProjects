using BlazorServerTest.Data.Entities;

namespace BlazorServerTest.Data.Repositories.Interfaces;
public interface IWeatherForecastRepository : IBaseRepository<WeatherForecast>
{
    Task<DtResponce<WeatherForecast>> LoadTable(DtParameters dtParameters);
}
