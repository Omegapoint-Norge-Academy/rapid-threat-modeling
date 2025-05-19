using Microsoft.EntityFrameworkCore;
using Rtm.Database;
using Rtm.Database.Models;

namespace Rtm.Worker.Services;

public class CreditCardInfoService(CommercialContext dbContext, ILogger<CreditCardInfoService> logger)
{
    public async Task<IEnumerable<CreditCardInfo>> GetAllCreditCardInfosAsync()
    {
        logger.LogInformation("Fetching all records in CreditCardInfos table");
        return await dbContext.CreditCardInfos.ToListAsync();
    }
}