using Catalog.Api.Modules.Product.Dtos;
using Entity.DbContexts.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Modules.Product.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CatalogDbContext _context;
    
    public ProductRepository(CatalogDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ProductItemDto>> GetAllProductsAsync()
    {
        return await _context.Products.Select(s => new ProductItemDto
        {
            ProductId = s.ProductId,
            Name = s.Name,
            Price = s.Price,
            Image = s.Image
        }).ToListAsync();
    }
    
    public async Task<ProductItemDto> GetProductByIdAsync(int productId)
    {
        var product = await _context.Products
            .Where(s => s.ProductId == productId)
            .Select(s => new ProductItemDto
            {
                ProductId = s.ProductId,
                Name = s.Name,
                Price = s.Price,
                Image = s.Image
            })
            .FirstOrDefaultAsync();
        
        if (product == null)
        {
            throw new KeyNotFoundException();
        }
        
        return product;
    }
    
    public async Task RemoveProductByIdAsync(int productId)
    {
        var productToRemove = await _context.Products
            .Where(s => s.ProductId == productId)
            .FirstOrDefaultAsync();

        if (productToRemove == null)
        {
            throw new KeyNotFoundException();
        }
        
        _context.Products.Remove(productToRemove);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateProductAsync(ProductItemDto product)
    {
        var productToUpdate = await _context.Products
            .Where(s => s.ProductId == product.ProductId)
            .FirstAsync();

        productToUpdate.Name = product.Name;
        productToUpdate.Price = product.Price;
        productToUpdate.Image = product.Image;

        _context.Products.Update(productToUpdate);
        await _context.SaveChangesAsync();
    }
    
    public async Task<ProductItemDto> AddProductAsync(ProductItemDto product)
    {
        var productToAdd = new Entity.DbContexts.Catalog.Entities.Product
        {
            Name = product.Name,
            Price = product.Price,
            Image = product.Image
        };

        _context.Products.Add(productToAdd);
        await _context.SaveChangesAsync();

        product.ProductId = productToAdd.ProductId;
        
        return product;
    }
}
