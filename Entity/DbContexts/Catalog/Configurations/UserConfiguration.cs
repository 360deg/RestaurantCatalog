using Entity.DbContexts.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.DbContexts.Catalog.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(s => s.UserId);
        
        builder
            .Property(s => s.UserId)
            .ValueGeneratedOnAdd();

        builder
            .Property(s => s.UserName)
            .HasMaxLength(128);
        
        builder
            .Property(s => s.Password)
            .HasMaxLength(128);
    }
}
