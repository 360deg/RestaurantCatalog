namespace Catalog.Api.Modules.Product.Dtos;

public class ProductItemDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
}