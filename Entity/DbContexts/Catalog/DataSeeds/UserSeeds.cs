using Entity.DbContexts.Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entity.DbContexts.Catalog.DataSeeds;

public static class UserSeeds
{
    public static void SeedUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, UserName = "Test", Password = "AJtgAY3jcFkKbdUx3Ppweab0Rj2YBGW6bzphfUcwNSMH7qiNSkqtnYrw0FZcM2EAMw=="}
        );
    }
}
