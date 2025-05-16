using Rtm.Database.Models;

namespace Rtm.Database.SeedData;

public class SeedProducts
{
    public List<Product> Generate() => _products.ToList();

    private readonly IEnumerable<Product> _products =
    [
        new()
        {
            Name = "Himalaya salt",
            Price = 69m,
        },
        new()
        {
            Name = "Norton antivirus",
            Price = 29.90m,
        },
        new()
        {
            Name = "Horse armor",
            Price = 1000m,
        },
        new()
        {
            Name = "Mithril plated HDMI cable",
            Price = 20_000m,
        },
        new()
        {
            Name = "100 vbucks",
            Price = 100m,
        },
        new()
        {
            Name = "NordVPN subscription",
            Price = 90m,
        },
        new()
        {
            Name = "WinRar license",
            Price = 299m,
        },
        new()
        {
            Name = "Healing crystal",
            Price = 198m,
        },
        new()
        {
            Name = "Docker desktop commercial license",
            Price = 399.98m,
        },
        new()
        {
            Name = "YouTube premium",
            Price = 98.0m,
        },
        new()
        {
            Name = "Cigarettes",
            Price = 190.50m,
        },
    ];
}