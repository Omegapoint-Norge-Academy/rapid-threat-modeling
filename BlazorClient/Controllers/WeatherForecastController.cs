using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rtm.BlazorClient.Models;
using Rtm.BlazorClient.Services;

namespace Rtm.BlazorClient.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController(WeatherForecastCacheService weatherForecastCacheService) : ControllerBase
{
    [HttpGet(Name = "GetAllWeatherForecasts")]
    public ActionResult<IEnumerable<WeatherForecastSeriesModel>> GetAll()
    {
        return Ok(weatherForecastCacheService.GetWeatherForecastSeries());
    }

    [HttpPost(Name = "PostWeatherForecast")]
    public ActionResult Post([FromBody] IEnumerable<WeatherForecastModel> forecasts)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Console.WriteLine($"Received weather forecast data at {DateTime.Now.TimeOfDay}");
        weatherForecastCacheService.UpdateWeatherData(forecasts);

        return Ok();
    }
}