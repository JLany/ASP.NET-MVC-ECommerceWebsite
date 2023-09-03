using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIECommerce.Data.Models;

public class Product
{
    public int Id { get; set; }

    public string SellerId { get; set; } = null!;
    public virtual ITIECommerceUser Seller { get; set; } = null!;

    [MinLength(2)]
    [MaxLength(200)]
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    [Range(0.05, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
    public string? ImageUri { get; set; }

    public virtual ICollection<OrderEntry>? OrderEntries { get; set; }

    public Product()
    {
        
    }

    public Product(dynamic viewModel)
    {
        Id = viewModel.Id;
        SellerId = viewModel.SellerId;
        Name = viewModel.Name;
        Description = viewModel.Description;
        Price = viewModel.Price;
        Quantity = viewModel.Quantity;
        ImageUri = viewModel.ImageUri;
    }
}
