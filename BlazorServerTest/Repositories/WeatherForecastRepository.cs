using BlasorServerTest.Repositories.Interfaces;
using BlazorServerTest.Data;
using BlazorServerTest.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTest.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly AppDbContext _context;

        public WeatherForecastRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WeatherForecast> Add(WeatherForecast entity)
        {
            await _context.WeatherForecasts.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<WeatherForecast> Update(WeatherForecast entity)
        {
            //_context.WeatherForecasts.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

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
