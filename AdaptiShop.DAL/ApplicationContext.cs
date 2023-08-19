using AdaptiShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdaptiShop.DAL;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
        Database.EnsureCreated();
    }

    public DbSet<UserEntity?> Users { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<HistoryOfPurchaseEntity> Histories { get; set; }
}