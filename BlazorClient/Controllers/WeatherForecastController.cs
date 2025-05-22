using Microsoft.AspNetCore.Mvc;
using Rtm.BlazorClient.Models;
using Rtm.BlazorClient.Services;

namespace Rtm.BlazorClient.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController(HybridCacheService hybridCacheService, ILogger<WeatherForecastController> logger)
    : ControllerBase
{
    [HttpGet(Name = "GetAllWeatherForecasts")]
    public async Task<IResult> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("Received GET /api/weatherforecast");
        var weatherForecast = await hybridCacheService.GetWeatherForecastSeriesAsync(cancellationToken);
        return Results.Ok(weatherForecast);
    }

    [HttpPost(Name = "PostWeatherForecast")]
    public async Task<IResult> Post([FromBody] IEnumerable<WeatherForecastModel> forecasts)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Invalid json body in POST to /api/weatherforecast");
            return Results.BadRequest(ModelState);
        }

        logger.LogInformation($"Received weather forecasts");
        await hybridCacheService.SetWeatherDataAsync(forecasts);

        return Results.Created();
    }
}