using Entity.DbContexts.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Extensions;

public static class DatabaseExtensions
{
    public static void AddDatabaseContexts(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<CatalogDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CatalogConnection"));
        });
    }
}
