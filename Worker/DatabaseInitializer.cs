using Rtm.Database.SeedData;

namespace Rtm.Worker;

public static class DatabaseInitializer
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbSeeder = scope.ServiceProvider.GetService<DatabaseSeeder>();
        if (dbSeeder is not null)
            await dbSeeder.SeedDatabaseAsync();
    }
}