using Rtm.Database.Models;

namespace Rtm.Database.SeedData;

public class SeedProducts
{
    public List<Product> Generate() => _products.ToList();

    private readonly IEnumerable<Product> _products =
    [
        new()
        {
            Name = "Ringbrynje",
            Price = 20_000m,
        },
        new()
        {
            Name = "20 i 1 såpe",
            Price = 29.90m,
        },
        new()
        {
            Name = "Råolje (fat)",
            Price = 69m,
        },
        new()
        {
            Name = "Hundre vbucks",
            Price = 100m,
        },
        new()
        {
            Name = "5G-basestasjon",
            Price = 9_995m,
        },
        new()
        {
            Name = "WinRar-lisens",
            Price = 299m,
        },
        new()
        {
            Name = "Papegøye",
            Price = 198m,
        },
        new()
        {
            Name = "CD-spiller",
            Price = 399.98m,
        },
        new()
        {
            Name = "Solbærtoddy",
            Price = 100m,
        },
    ];
}
    