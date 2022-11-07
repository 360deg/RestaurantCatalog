using AuthService.Repositories;
using AuthService.Services;
using Catalog.Api.Modules.Product.Repositories;
using Catalog.Api.Modules.Product.Services;

namespace Catalog.Api.Extensions;

public static class DIEstensions
{
    public static void AddDI(this IServiceCollection service)
    {
        service.AddTransient<IProductRepository, ProductRepository>();
        service.AddTransient<IAuthRepository, AuthRepository>();
        
        service.AddTransient<IProductService, ProductService>();
        service.AddTransient<IAuthService, AuthService.Services.AuthService>();
    }
}
