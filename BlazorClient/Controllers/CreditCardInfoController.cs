using Microsoft.AspNetCore.Mvc;
using Rtm.BlazorClient.Models;
using Rtm.BlazorClient.Services;

namespace Rtm.BlazorClient.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditCardInfoController(CreditCardInfoCacheService creditCardInfoCacheService) : ControllerBase
{
    [HttpGet(Name = "GetAllCreditCardInfos")]
    public ActionResult<IEnumerable<CreditCardInfoModel>> GetAll()
    {
        return Ok(creditCardInfoCacheService.GetCreditCardInfos());
    }

    [HttpPost(Name = "PostCreditCardInfo")]
    public ActionResult Post([FromBody] IEnumerable<CreditCardInfoModel> creditCardInfos)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Console.WriteLine($"Received credit card info at {DateTime.Now.TimeOfDay}");
        creditCardInfoCacheService.UpdateCreditCardInfos(creditCardInfos);

        return Ok();
    }
}