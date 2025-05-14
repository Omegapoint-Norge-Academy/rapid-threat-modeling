using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

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
}