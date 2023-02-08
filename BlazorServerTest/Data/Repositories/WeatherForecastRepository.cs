using BlazorServerTest.Data.Entities;
using BlazorServerTest.Data.Infrastructure;
using BlazorServerTest.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTest.Data.Repositories
{
    public class WeatherForecastRepository : BaseRepository<WeatherForecast>, IWeatherForecastRepository
    {
        private readonly ILogger<IWeatherForecastRepository> _logger;

        public WeatherForecastRepository(AppDbContext context, ILogger<IWeatherForecastRepository> logger) : base(context)
        {
            _logger = logger;
        }

        // No obviously reasons to override basic method by it only for showing a different approach.
        public override Task<WeatherForecast> Update(WeatherForecast entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            _logger.LogInformation($"Updated entity {entity}");

            return _context.SaveChangesAsync().ContinueWith(x => entity);
        }
    }
}
