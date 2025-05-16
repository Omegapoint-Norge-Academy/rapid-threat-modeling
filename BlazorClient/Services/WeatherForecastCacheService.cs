using Rtm.BlazorClient.Models;

namespace Rtm.BlazorClient.Services;

public class WeatherForecastCacheService
{
    private readonly Lock _weatherLock = new();
    private WeatherForecastSeriesModel _weatherForecastSeriesModel =
        new() { WeatherForecasts = [], Timestamp = DateTime.Now };

    public event Action? OnChange;

    public WeatherForecastSeriesModel GetWeatherForecastSeries()
    {
        return _weatherForecastSeriesModel;
    }

    public void UpdateWeatherData(IEnumerable<WeatherForecastModel> newData)
    {
        lock (_weatherLock)
        {
            _weatherForecastSeriesModel = new WeatherForecastSeriesModel
            {
                WeatherForecasts = (newData ?? [])
                    .OrderBy(f => f.Date)
                    .ToList(),
                Timestamp = DateTime.Now,
            };
        }

        OnChange?.Invoke();
    }
}