using BlazorServerTest.Data;
using BlazorServerTest.Data.Infrastructure;
using BlazorServerTest.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTest.Data.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<IWeatherForecastRepository> _logger;

        public WeatherForecastRepository(AppDbContext context, ILogger<IWeatherForecastRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<WeatherForecast> Add(WeatherForecast entity)
        {
            await _context.WeatherForecasts.AddAsync(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Added entity {entity}");

            return entity;
        }

        public async Task<WeatherForecast> Update(WeatherForecast entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Updated entity {entity}");

            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await Get(id);
            if (entity is not null)
            {
                _context.WeatherForecasts.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<WeatherForecast> Get(int id)
        {
            return await _context.WeatherForecasts.FindAsync(id);
            //.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<WeatherForecast>> GetAll()
        {
            return await _context.WeatherForecasts.ToListAsync();
        }
    }
}
