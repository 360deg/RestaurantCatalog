using Entity.DbContexts.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.DbContexts.Catalog.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .HasKey(s => s.ProductId);
        
        builder
            .Property(s => s.ProductId)
            .ValueGeneratedOnAdd();

        builder
            .Property(s => s.Name)
            .HasMaxLength(100);
    }
}
