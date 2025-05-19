using Rtm.Worker.Models;

namespace Rtm.Worker.Services;

public class WeatherForecastService(ILogger<WeatherForecastService> logger)
{
    private static readonly string[] Summaries =
    [
        "Dritkaldt", "Veldig kaldt", "Kaldt", "Kjølig", "Mildt", "Lummert", "Varmt", "Hett", "Dritvarmt"
    ];

    public IEnumerable<WeatherForecastModel> GetForecasts(int days)
    {
        logger.LogInformation("Generating and returning {days} weather forecasts", days);

        var minTemp = -20m;
        var maxTemp = 55m;

        if (days < 1 || days > 100)
            return [];
        return Enumerable.Range(1, days).Select(index =>
            {
                var tempC = Random.Shared.Next((int)minTemp, (int)maxTemp);
                return new WeatherForecastModel
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = tempC,
                    Summary = Summaries[(int)((tempC - minTemp) * (Summaries.Length - 1) / (maxTemp - minTemp))]
                };
            })
            .ToArray();
    }
}