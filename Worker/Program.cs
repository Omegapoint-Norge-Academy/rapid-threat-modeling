using Rtm.Database;
using Rtm.Database.SeedData;
using Rtm.Worker;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<DatabaseSeeder>();

builder.Services.AddDbContext<CommercialContext>(options =>
{
    var connectionsString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionsString,
        s => s.MigrationsAssembly(typeof(CommercialContext).Assembly));
});

builder.Services.AddScoped<DatabaseSeeder>();

var app = builder.Build();

await app.InitializeDatabaseAsync();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();