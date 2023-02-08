using BlazorServerTest.Data.Entities.Interfaces;

namespace BlazorServerTest.Data.Entities;
public class WeatherForecast : IEntity
{
    public int Id { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public int TemperatureC { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string? Summary { get; set; }
    public bool IsChecked { get; set; }
}
