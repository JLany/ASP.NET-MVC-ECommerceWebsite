using ITIECommerce.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace ITIECommerce.Web.Models;

public class ProductViewModel
{
    public int? Id { get; set; }

    public string? SellerId { get; set; } 
    public string? SellerName { get; set; }

    [MinLength(2)]
    [MaxLength(200)]
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    [Range(0.05, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
    public string? ImageUri { get; set; }
    public IFormFile? Image { get; set; }

    public ProductViewModel()
    {
        
    }

    public ProductViewModel(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
        Quantity = product.Quantity;
        ImageUri = product.ImageUri;
        SellerId = product.SellerId;
        SellerName = product.Seller?.FullName;
    }
}
