using Microsoft.EntityFrameworkCore;
using Rtm.Database.Models;

namespace Rtm.Database;

public class CommercialContext : DbContext
{
    public CommercialContext(DbContextOptions<CommercialContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<CreditCardInfo> CreditCardInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);
    }
}