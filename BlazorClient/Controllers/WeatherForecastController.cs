using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rtm.BlazorClient.Models;

namespace Rtm.BlazorClient.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController:ControllerBase
{
    [AllowAnonymous]
    [HttpPost(Name = "PostWeatherForecast")]
    public ActionResult Post([FromBody] WeatherForecast forecast)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        Console.WriteLine(forecast.Summary);
        return Ok();
    }
}