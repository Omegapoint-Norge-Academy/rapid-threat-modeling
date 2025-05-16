namespace Rtm.BlazorClient.Models;

public class WeatherForecastSeriesModel
{
    public required List<WeatherForecastModel> WeatherForecasts { get; init; }
    public required DateTime Timestamp { get; init; }
}