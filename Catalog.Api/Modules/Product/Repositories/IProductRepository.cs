using Catalog.Api.Modules.Product.Dtos;

namespace Catalog.Api.Modules.Product.Repositories;

public interface IProductRepository
{
    Task<List<ProductItemDto>> GetAllProductsAsync();
    Task<ProductItemDto> GetProductByIdAsync(int productId);
    Task RemoveProductByIdAsync(int productId);
    Task UpdateProductAsync(ProductItemDto product);
    Task<ProductItemDto> AddProductAsync(ProductItemDto product);
}
