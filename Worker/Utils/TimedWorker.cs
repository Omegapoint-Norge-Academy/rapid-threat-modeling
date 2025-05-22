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

    private readonly string? _nodeClientBaseUrl =
        appConfiguration.GetValue<string>("TimedWorker:NodeClientBaseUrl");

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

        var blazorTask = SendPayloadToClientAsync(_blazorClientBaseUrl, "weatherforecast", content, cancellationToken);
        var nodeTask = SendPayloadToClientAsync(_nodeClientBaseUrl, "weatherforecast", content, cancellationToken);

        await Task.WhenAll(blazorTask, nodeTask);
    }

    private async Task SendCreditCardInfoAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var creditCardInfoService = scope.ServiceProvider.GetRequiredService<CreditCardInfoService>();

        var payload = await creditCardInfoService.GetAllCreditCardInfosAsync();
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var blazorTask = SendPayloadToClientAsync(_blazorClientBaseUrl, "creditcardinfo", content, cancellationToken);
        var nodeTask = SendPayloadToClientAsync(_nodeClientBaseUrl, "creditcardinfo", content, cancellationToken);

        await Task.WhenAll(blazorTask, nodeTask);
    }

    private async Task SendPayloadToClientAsync(string? baseUrl, string resourceUrl, StringContent body,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
            return;
        var requestUri = $"{baseUrl}/{resourceUrl}";
        logger.LogInformation($"Sending POST to {requestUri}");
        var response = await _httpClient.PostAsync(requestUri, body, cancellationToken);
        logger.LogInformation("POST {RequestUri} response code: {ResponseStatusCode}", requestUri, response.StatusCode);
    }
}