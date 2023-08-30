using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIECommerce.Data.Models;

public class Order
{
    public int Id { get; set; }

    public string CustomerId { get; set; } = null!;
    public virtual ITIECommerceUser Customer { get; set; } = null!;

    [MinLength(5)]
    [MaxLength(200)]
    public string Address { get; set; } = null!;

    [Range(0, double.MaxValue)]
    public decimal Total { get; set; }
}
