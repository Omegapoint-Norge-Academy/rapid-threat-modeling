using Rtm.Database;
using Rtm.Database.SeedData;
using Microsoft.EntityFrameworkCore;
using Rtm.Worker.Services;
using Rtm.Worker.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<CommercialContext>(options =>
{
    var connectionsString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionsString,
        s => s.MigrationsAssembly(typeof(CommercialContext).Assembly));
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped<DatabaseSeeder>();
}

builder.Services.AddScoped<WeatherForecastService>();
builder.Services.AddScoped<CreditCardInfoService>();
builder.Services.AddHostedService<TimedWorker>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();