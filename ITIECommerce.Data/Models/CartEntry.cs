using System.ComponentModel.DataAnnotations;

namespace ITIECommerce.Data.Models;

public class CartEntry<TCart>
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    public string CartId { get; set; }
    public virtual TCart Cart { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0, double.MaxValue)]
    public decimal SubTotal { get; set; }
}