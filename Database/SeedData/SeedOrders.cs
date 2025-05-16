using Rtm.Database.Models;

namespace Rtm.Database.SeedData;

public class SeedOrders(int seed = 123)
{
    private readonly Random _random = new(seed);

    public List<Order> GenerateFromUsersAndProducts(List<User> users, List<Product> products, int count)
    {
        var result = new List<Order>();

        for (var i = 0; i < count; i++)
        {
            result.Add(new Order
            {
                UserId = users[_random.Next(users.Count - 1)].Id,
                ProductId = products[_random.Next(products.Count - 1)].Id,
            });
        }

        return result;
    }
}