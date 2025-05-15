using Rtm.BlazorClient.Models;

namespace Rtm.BlazorClient.Services;

public class CachedWeatherDataService
{
    private readonly Lock _weatherLock = new();
    private IEnumerable<WeatherForecast>? _weatherData;
    private DateTime? _weatherDate;

    public event Action? OnChange;

    public WeatherForecastSeries GetWeatherForecastSeries()
    {
        return new WeatherForecastSeries
        {
            WeatherForecasts = (_weatherData ?? [])
                .OrderBy(f => f.Date)
                .ToList(),
            Timestamp = _weatherDate ?? DateTime.Now,
        };
    }

    public void UpdateWeatherData(IEnumerable<WeatherForecast> newData)
    {
        lock (_weatherLock)
        {
            _weatherData = newData;
            _weatherDate = DateTime.Now;
        }
        OnChange?.Invoke();
    }
}