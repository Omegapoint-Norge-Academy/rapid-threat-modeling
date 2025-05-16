using System.Text;
using System.Text.Json;
using Rtm.Worker.Services;

namespace Rtm.Worker;

public class TimedWorker(IServiceProvider serviceProvider) : BackgroundService
{
    private const string ApiUrl = "https://localhost:6969/api";
    private const int DelaySeconds = 10;
    private readonly HttpClient _httpClient = new();

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var weatherTask = SendWeatherForecastAsync(cancellationToken);
            var creditCardTask = SendCreditCardInfoAsync(cancellationToken);

            await Task.WhenAll(weatherTask, creditCardTask);

            await Task.Delay(TimeSpan.FromSeconds(DelaySeconds), cancellationToken);
        }
    }

    private async Task SendWeatherForecastAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var weatherForecastService = scope.ServiceProvider.GetRequiredService<WeatherForecastService>();

        var payload = weatherForecastService.GetForecasts(5);
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var requestUri = $"{ApiUrl}/weatherforecast";

        await _httpClient.PostAsync(requestUri, content, cancellationToken);
    }

    private async Task SendCreditCardInfoAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var creditCardInfoService = scope.ServiceProvider.GetRequiredService<CreditCardInfoService>();

        var payload = await creditCardInfoService.GetAllCreditCardInfosAsync();
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var requestUri = $"{ApiUrl}/creditcardinfo";

        await _httpClient.PostAsync(requestUri, content, cancellationToken);
    }
}