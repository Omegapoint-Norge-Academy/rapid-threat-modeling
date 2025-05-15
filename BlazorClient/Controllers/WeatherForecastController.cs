using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rtm.BlazorClient.Models;
using Rtm.BlazorClient.Services;

namespace Rtm.BlazorClient.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController(CachedWeatherDataService cachedWeatherDataService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost(Name = "PostWeatherForecast")]
    public ActionResult Post([FromBody] IEnumerable<WeatherForecast> forecasts)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Console.WriteLine($"Received weather forecast data at {DateTime.Now.TimeOfDay}");
        cachedWeatherDataService.UpdateWeatherData(forecasts);

        return Ok();
    }
}