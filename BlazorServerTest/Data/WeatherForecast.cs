using BlazorServerTest.Data;

namespace BlazorServerTest.Data;
public class WeatherForecast
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
		public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string? Summary { get; set; }
        public bool IsChecked { get; set; }
    }