using BlazorServerTest.Data;

namespace BlazorServerTest.Data.Repositories.Interfaces
{
    public interface IWeatherForecastRepository
    {
        Task<WeatherForecast> Add(WeatherForecast entity);
        Task<WeatherForecast> Update(WeatherForecast entity);
        Task Delete(int id);
        Task<WeatherForecast> Get(int id);
        Task<List<WeatherForecast>> GetAll();
    }
}
