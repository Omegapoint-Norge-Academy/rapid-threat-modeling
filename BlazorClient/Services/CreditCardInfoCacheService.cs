using Rtm.BlazorClient.Models;

namespace Rtm.BlazorClient.Services;

public class CreditCardInfoCacheService
{
    private readonly Lock _cacheLock = new();
    private IEnumerable<CreditCardInfoModel> _cachedData = [];

    public IEnumerable<CreditCardInfoModel> GetCreditCardInfos()
    {
        return _cachedData;
    }

    public void UpdateCreditCardInfos(IEnumerable<CreditCardInfoModel> newData)
    {
        lock (_cacheLock)
        {
            _cachedData = newData;
        }
    }
}