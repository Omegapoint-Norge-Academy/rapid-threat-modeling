using Microsoft.AspNetCore.Mvc;
using Rtm.BlazorClient.Models;
using Rtm.BlazorClient.Services;

namespace Rtm.BlazorClient.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditCardInfoController(
    HybridCacheService cacheService,
    ILogger<CreditCardInfoController> logger
) : ControllerBase
{
    [HttpGet(Name = "GetAllCreditCardInfos")]
    public async Task<IResult> GetAll()
    {
        logger.LogInformation("Received GET /api/creditcardinfo");
        var ccInfo = await cacheService.GetCreditCardInfoAsync();
        return Results.Ok(ccInfo);
    }

    [HttpPost(Name = "PostCreditCardInfo")]
    public async Task<IResult> Post([FromBody] IEnumerable<CreditCardInfoModel> creditCardInfos)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Invalid json body in POST to /api/creditcardinfo");
            return Results.BadRequest(ModelState);
        }

        logger.LogInformation($"Received credit card info");
        await cacheService.SetCreditCardInfoAsync(creditCardInfos);

        return Results.Created();
    }
}