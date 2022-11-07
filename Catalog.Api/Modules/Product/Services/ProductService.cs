using Catalog.Api.Modules.Product.Dtos;
using Catalog.Api.Modules.Product.Repositories;
using Common.Exceptions;
using Common.Handlers;
using EmailService.RabbitMQ;

namespace Catalog.Api.Modules.Product.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IRabbitMqService _rabbitMqService;
    
    public ProductService(IProductRepository productRepository, IRabbitMqService rabbitMqService)
    {
        _productRepository = productRepository;
        _rabbitMqService = rabbitMqService;
    }

    public async Task<List<ProductItemDto>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }

    public async Task<ProductItemDto> GetProductByIdAsync(int productId)
    {
        return await _productRepository.GetProductByIdAsync(productId);
    }

    public async Task RemoveProductByIdAsync(int productId)
    {
        await _productRepository.RemoveProductByIdAsync(productId);
    }

    public async Task UpdateProductAsync(ProductItemDto product)
    {
        if (!Base64Handler.IsValueValid(product.Image))
        {
            throw new BadRequestException("Failed to update product with wrong Image base64 format");
        }
        await _productRepository.UpdateProductAsync(product);
    }

    public async Task<ProductItemDto> AddProductAsync(ProductItemDto product)
    {
        if (!Base64Handler.IsValueValid(product.Image))
        {
            throw new BadRequestException("Failed to add product with wrong Image base64 format");
        }
        var newProduct = await _productRepository.AddProductAsync(product);

        _rabbitMqService.SendMessage($"{product.Name} with price {product.Price} has been added.");
        
        return newProduct;
    }
}
