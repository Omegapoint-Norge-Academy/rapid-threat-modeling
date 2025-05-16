namespace Rtm.Worker.Models;

public class WeatherForecastModel
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}