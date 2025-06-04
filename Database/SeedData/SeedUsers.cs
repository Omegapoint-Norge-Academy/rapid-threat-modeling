using Bogus;
using Rtm.Database.Models;

namespace Rtm.Database.SeedData;

public class SeedUsers
{
    private readonly Faker<User> _faker;

    public SeedUsers(int seed = 123)
    {
        _faker = new Faker<User>("en_GB")
            .UseSeed(seed)
            .RuleFor(cc => cc.FirstName, f => f.Name.FirstName())
            .RuleFor(cc => cc.LastName, f => f.Name.LastName())
            .RuleFor(cc => cc.Email, f => f.Internet.Email());
    }

    public List<User> GenerateForCreditCardInfos(List<CreditCardInfo> ccInfos)
    {
        var users = _faker.Generate(ccInfos.Count);
        for (var i = 0; i < users.Count; i++)
        {
            var names = ccInfos[i].Owner.Split(" ");
            users[i].CreditCardId = ccInfos[i].Id;
            users[i].FirstName = string.Join(" ", names.Take(names.Length - 1));
            users[i].LastName = names.Last();
        }

        return users;
    }
}