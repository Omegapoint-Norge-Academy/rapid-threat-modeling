using Microsoft.Extensions.Caching.Hybrid;
using Rtm.BlazorClient.Models;

namespace Rtm.BlazorClient.Services;

public class HybridCacheService(HybridCache cache)
{
    private const string WeatherCacheKey = "WeatherForecast";
    private const string CreditCardCacheKey = "CreditCardInfo";

    public event Action? OnWeatherForecastSeriesChange;

    public async Task<IEnumerable<CreditCardInfoModel>> GetCreditCardInfoAsync(CancellationToken token = default)
    {
        return await cache.GetOrCreateAsync<IEnumerable<CreditCardInfoModel>>(CreditCardCacheKey,
            async _ => await Task.FromResult<IEnumerable<CreditCardInfoModel>>([]),
            cancellationToken: token);
    }

    public async Task<WeatherForecastSeriesModel?> GetWeatherForecastSeriesAsync(CancellationToken token = default)
    {
        return await cache.GetOrCreateAsync<WeatherForecastSeriesModel?>(WeatherCacheKey,
            async _ => await Task.FromResult<WeatherForecastSeriesModel?>(new WeatherForecastSeriesModel
            {
                WeatherForecasts = [],
                Timestamp = DateTime.UtcNow
            }),
            cancellationToken: token);
    }

    public async Task SetCreditCardInfoAsync(IEnumerable<CreditCardInfoModel> newData)
    {
        await cache.SetAsync(CreditCardCacheKey, newData);
    }

    public async Task SetWeatherDataAsync(IEnumerable<WeatherForecastModel> newData)
    {
        var weatherForecastSeriesModel = new WeatherForecastSeriesModel
        {
            WeatherForecasts = newData
                .OrderBy(f => f.Date)
                .ToList(),
            Timestamp = DateTime.UtcNow,
        };

        await cache.SetAsync(WeatherCacheKey, weatherForecastSeriesModel);

        OnWeatherForecastSeriesChange?.Invoke();
    }
}