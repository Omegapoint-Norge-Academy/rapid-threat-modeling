namespace Rtm.Database.SeedData;

public class DatabaseSeeder(CommercialContext dbContext)
{
    public async Task SeedDatabaseAsync()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();

        var numUsers = 10;
        
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