using Microsoft.EntityFrameworkCore;
using Rtm.Database;

namespace Rtm.BlazorClient.Extensions;

public static class DatabaseServiceExtensions
{
    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<CommercialContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString,
                    s => s.MigrationsAssembly(typeof(CommercialContext).Assembly));
            }
        );
    }
}