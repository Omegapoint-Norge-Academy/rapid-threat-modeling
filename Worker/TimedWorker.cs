using System.Text;
using System.Text.Json;
using Rtm.Worker.Services;

namespace Rtm.Worker;

public class TimedWorker(IServiceProvider serviceProvider) : BackgroundService
{
    private const string ApiUrl = "https://localhost:6969/api";
    private readonly HttpClient _httpClient = new();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var weatherForecastService = scope.ServiceProvider.GetRequiredService<WeatherForecastService>();
            
            var payload = weatherForecastService.GetForecasts(5);
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var requestUri = $"{ApiUrl}/weatherforecast";

            Console.WriteLine($"Sending updated forecast to {requestUri}");
            await _httpClient.PostAsync(requestUri, content, stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}