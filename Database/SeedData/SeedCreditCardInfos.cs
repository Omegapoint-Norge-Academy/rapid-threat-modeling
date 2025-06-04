using Bogus;
using Rtm.Database.Models;

namespace Rtm.Database.SeedData;

public class SeedCreditCardInfos
{
    public List<CreditCardInfo> Generate() => _creditCardInfos.ToList();

    private readonly IEnumerable<CreditCardInfo> _creditCardInfos =
    [
        new()
        {
            Owner = "Kåre Vinneren",
            Number = "4242 4242 4242 4242",
            Cvc = "211",
            ExpirationMonth = 1,
            ExpirationYear = 2027
        },
        new()
        {
            Owner = "Per Vers",
            Number = "1111 2222 3333 4444",
            Cvc = "980",
            ExpirationMonth = 12,
            ExpirationYear = 2025
        },
        new()
        {
            Owner = "Kjell T. Ringen",
            Number = "5555 4193 5901 3094",
            Cvc = "246",
            ExpirationMonth = 10,
            ExpirationYear = 2026
        },
        new()
        {
            Owner = "Jan T. Loven",
            Number = "3782 9301 0032 4231",
            Cvc = "601",
            ExpirationMonth = 2,
            ExpirationYear = 2028
        },
        new()
        {
            Owner = "Unni Versell",
            Number = "4240 3023 0983 1200",
            Cvc = "931",
            ExpirationMonth = 5,
            ExpirationYear = 2028
        },
        new()
        {
            Owner = "Jørn E. Steen",
            Number = "6011 0009 9013 9424",
            Cvc = "214",
            ExpirationMonth = 8,
            ExpirationYear = 2026
        }
    ];
}