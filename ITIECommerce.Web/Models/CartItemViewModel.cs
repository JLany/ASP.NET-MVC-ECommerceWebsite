using System.ComponentModel.DataAnnotations;

namespace ITIECommerce.Web.Models;

public class CartItemViewModel
{
    public int ProductId { get; set; }

    [Range(1, 9999)]
    public int Quantity { get; set; }
}
