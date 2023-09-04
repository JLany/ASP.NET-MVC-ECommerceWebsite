using ITIECommerce.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace ITIECommerce.Web.Models;

public class OrderViewModel
{
    public int Id { get; set; }

    public string? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string CustomerEmailAddress { get; set; }
    public string? CustomerPhoneNumber { get; set; }

    [MinLength(5)]
    [MaxLength(200)]
    public string Address { get; set; } = null!;

    public decimal SubTotal { get; set; }
    public decimal ShippingCost { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Total { get; set; }

    public DateTime CreateDate { get; set; }
    public OrderStatus Status { get; set; }

    public IEnumerable<OrderEntry> OrderEntries { get; set; }

    public OrderViewModel()
    {
        
    }

    public OrderViewModel(Order order)
    {
        Id = order.Id;
        CustomerId = order.CustomerId;
        CustomerName = order.Customer?.FullName;
        CustomerEmailAddress = order.Customer?.UserName!;
        CustomerPhoneNumber = order.Customer?.PhoneNumber;
        Address = order.Address;
        SubTotal = order.SubTotal;
        ShippingCost = order.ShippingCost;
        Total = order.Total;
        CreateDate = order.CreateDate.AddHours(3);
        OrderEntries = order.OrderEntries!;
    }
}
