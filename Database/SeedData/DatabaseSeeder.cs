using Microsoft.Extensions.Logging;

namespace Rtm.Database.SeedData;

public class DatabaseSeeder(CommercialContext dbContext, ILogger<DatabaseSeeder> logger)
{
    public async Task SeedDatabaseAsync()
    {
        logger.LogInformation("Deleting development database");
        await dbContext.Database.EnsureDeletedAsync();
        logger.LogInformation("Creating new development database");
        await dbContext.Database.EnsureCreatedAsync();

        var numUsers = 10;

        logger.LogInformation("Seeding development database");
        // Create credit card info
        var ccInfos = new SeedCreditCardInfos().Generate(numUsers);
        await dbContext.CreditCardInfos.AddRangeAsync(ccInfos);

        // Save to db to generate ids
        await dbContext.SaveChangesAsync();

        // Create users
        var users = new SeedUsers().GenerateForCreditCardInfos(ccInfos);
        await dbContext.Users.AddRangeAsync(users);

        // Create products
        var products = new SeedProducts().Generate();
        await dbContext.Products.AddRangeAsync(products);

        // Save to db to generate ids
        await dbContext.SaveChangesAsync();

        // Create orders and save to db
        var orders = new SeedOrders().GenerateFromUsersAndProducts(users, products, 10);
        await dbContext.Orders.AddRangeAsync(orders);

        // Save to db to generate ids
        await dbContext.SaveChangesAsync();
    }
}