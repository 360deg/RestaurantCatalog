using Catalog.Api.Models;
using Catalog.Api.Modules.Product.Dtos;
using Catalog.Api.Modules.Product.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    /// <summary>
    /// Returns list of all products
    /// </summary>
    [HttpGet]
    [Route("")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<ProductItemDto>> GetProductList()
    {
        return await _productService.GetAllProductsAsync();
    }
    
    /// <summary>
    /// Returns product by it's id
    /// </summary>
    [HttpGet]
    [Route("{productId:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorHandlerResponse))]
    public async Task<ProductItemDto> GetProduct(int productId)
    {
        return await _productService.GetProductByIdAsync(productId);
    }
    
    /// <summary>
    /// Removes product by it's id
    /// </summary>
    [HttpDelete]
    [Route("")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorHandlerResponse))]
    public async Task RemoveProduct([FromQuery] int productId)
    {
        await _productService.RemoveProductByIdAsync(productId);
    }
    
    /// <summary>
    /// Updates product
    /// </summary>
    [HttpPut]
    [Route("")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorHandlerResponse))]
    public async Task UpdateProduct([FromBody] ProductItemDto product)
    {
        await _productService.UpdateProductAsync(product);
    }
    
    /// <summary>
    /// Adds new product
    /// </summary>
    [HttpPost]
    [Route("")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorHandlerResponse))]
    public async Task<ProductItemDto> AddProduct([FromBody] ProductItemDto product)
    {
        return await _productService.AddProductAsync(product);
    }
}
