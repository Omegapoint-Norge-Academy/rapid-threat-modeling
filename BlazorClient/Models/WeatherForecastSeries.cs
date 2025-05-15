namespace Rtm.BlazorClient.Models;

public class WeatherForecastSeries
{
    public required List<WeatherForecast> WeatherForecasts { get; init; }
    public required DateTime Timestamp { get; init; }
}