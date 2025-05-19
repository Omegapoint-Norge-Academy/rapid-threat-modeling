using System.Text;
using System.Text.Json;
using Rtm.Worker.Services;

namespace Rtm.Worker.Utils;

public class TimedWorker(
    IConfiguration appConfiguration,
    IServiceProvider serviceProvider,
    IHttpClientFactory httpClientFactory,
    ILogger<TimedWorker> logger
) : BackgroundService
{
    private readonly string? _blazorClientBaseUrl =
        appConfiguration.GetValue<string>("TimedWorker:BlazorClientBaseUrl");

    private readonly int _delaySeconds =
        appConfiguration.GetValue<int?>("TimedWorker:DelaySeconds") is int value && value > 0 ? value : 60;

    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Worker task with interval {seconds}s started", _delaySeconds);
        while (!cancellationToken.IsCancellationRequested)
        {
            var weatherTask = SendWeatherForecastAsync(cancellationToken);
            var creditCardTask = SendCreditCardInfoAsync(cancellationToken);

            await Task.WhenAll(weatherTask, creditCardTask);

            await Task.Delay(TimeSpan.FromSeconds(_delaySeconds), cancellationToken);
        }
    }

    private async Task SendWeatherForecastAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var weatherForecastService = scope.ServiceProvider.GetRequiredService<WeatherForecastService>();

        var payload = weatherForecastService.GetForecasts(5);
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var requestUri = $"{_blazorClientBaseUrl}/weatherforecast";

        logger.LogDebug("Sending weather forecast to BlazorClient");
        var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
        logger.LogDebug("Weather forecast POST response code: {ResponseStatusCode}", response.StatusCode);
    }

    private async Task SendCreditCardInfoAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var creditCardInfoService = scope.ServiceProvider.GetRequiredService<CreditCardInfoService>();

        var payload = await creditCardInfoService.GetAllCreditCardInfosAsync();
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var requestUri = $"{_blazorClientBaseUrl}/creditcardinfo";

        logger.LogDebug("Sending credit card info to BlazorClient");
        var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
        logger.LogDebug("Credit card info POST response code: {ResponseStatusCode}", response.StatusCode);
    }
}