using Bogus;
using Database.Models;

namespace Database.SeedData;

public class SeedCreditCardInfos
{
    private readonly Faker<CreditCardInfo> _faker;

    public SeedCreditCardInfos(int seed = 123)
    {
        _faker = new Faker<CreditCardInfo>("nb_NO")
            .UseSeed(seed)
            .RuleFor(cc => cc.Owner, f => f.Name.FullName())
            .RuleFor(cc => cc.Number, f => f.Finance.CreditCardNumber())
            .RuleFor(cc => cc.Cvc, f => f.Finance.CreditCardCvv())
            .RuleFor(cc => cc.ExpirationMonth, f => f.Random.Int(1, 12))
            .RuleFor(cc => cc.ExpirationYear, f => f.Random.Int(DateTime.Now.Year + 1, DateTime.Now.Year + 5));
    }

    public List<CreditCardInfo> Generate(int count) => _faker.Generate(count);
}