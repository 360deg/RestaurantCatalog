using Catalog.Api.Modules.Product.Dtos;

namespace Catalog.Api.Modules.Product.Services;

public interface IProductService
{
    Task<List<ProductItemDto>> GetAllProductsAsync();
    Task<ProductItemDto> GetProductByIdAsync(int productId);
    Task RemoveProductByIdAsync(int productId);
    Task UpdateProductAsync(ProductItemDto product);
    Task<ProductItemDto> AddProductAsync(ProductItemDto product);
}