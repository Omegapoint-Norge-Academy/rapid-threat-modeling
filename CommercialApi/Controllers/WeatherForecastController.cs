using Microsoft.AspNetCore.Mvc;

namespace CommercialApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Dritkaldt", "Veldig kaldt", "Kaldt", "Kjølig", "Mildt", "Lummert", "Varmt", "Hett", "Dritvarmt"
    ];

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var minTemp = -20m;
        var maxTemp = 55m;
        return Enumerable.Range(1, 5).Select(index =>
            {
                var tempC = Random.Shared.Next((int)minTemp, (int)maxTemp);
                return new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = tempC,
                    Summary = Summaries[(int)((tempC - minTemp) * (Summaries.Length - 1) / (maxTemp - minTemp))]
                };
            })
            .ToArray();
    }
}