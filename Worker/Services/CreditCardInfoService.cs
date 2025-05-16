using Microsoft.EntityFrameworkCore;
using Rtm.Database;
using Rtm.Database.Models;

namespace Rtm.Worker.Services;

public class CreditCardInfoService(CommercialContext dbContext)
{
    public async Task<IEnumerable<CreditCardInfo>> GetAllCreditCardInfosAsync()
    {
        return await dbContext.CreditCardInfos.ToListAsync();
    }
}