using BlazorServerTest.Data;

namespace BlasorServerTest.Repositories.Interfaces
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
