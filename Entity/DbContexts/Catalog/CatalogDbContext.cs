using Entity.DbContexts.Catalog.DataSeeds;
using Entity.DbContexts.Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entity.DbContexts.Catalog;

public class CatalogDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("CatalogTest");
        
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        modelBuilder.SeedProducts();
        modelBuilder.SeedUsers();
    }
}
