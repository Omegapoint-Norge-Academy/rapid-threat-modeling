using Rtm.BlazorClient.Models;

namespace Rtm.BlazorClient.Services;

public class CachedWeatherDataService
{
    private readonly Lock _weatherLock = new();
    private IEnumerable<WeatherForecast>? _weatherData;

    public IEnumerable<WeatherForecast> GetWeatherForecast() => _weatherData ?? [];

    public void UpdateWeatherData(IEnumerable<WeatherForecast> newData)
    {
        lock (_weatherLock)
        {
            _weatherData = newData;
        }
    }
}