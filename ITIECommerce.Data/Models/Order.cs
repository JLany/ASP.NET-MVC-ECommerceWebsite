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

    public decimal SubTotal { get; set; }
    public decimal ShippingCost { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Total { get; set; }

    public DateTime CreateDate { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Shipping;

    public virtual ICollection<OrderEntry>? OrderEntries { get; set; }

    public Order()
    {
        
    }

    public Order(dynamic viewModel)
    {
        Id = viewModel.Id;
        CustomerId = viewModel.CustomerId;
        Address = viewModel.Address;
        Total = viewModel.Total;
        CreateDate = viewModel.CreateDate;
    }
}
