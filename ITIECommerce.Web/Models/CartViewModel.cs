using ITIECommerce.Data.Models;

namespace ITIECommerce.Web.Models;

public class CartViewModel
{
    public string? CartId { get; set; }
    public Dictionary<Product, int> CartEntries { get; set; } = new();
    public decimal SubTotal { get; set; }
    public decimal ShippingCost { get; set; } = 20M;

    public CartViewModel()
    {
        
    }

    public CartViewModel(Cart cart) 
    {
        if (cart != null)
        {
            foreach (var entry in cart.CartEntries)
            {
                if (CartEntries.ContainsKey(entry.Product))
                {
                    CartEntries[entry.Product] += entry.Quantity;
                }
                else
                {
                    CartEntries.Add(entry.Product, entry.Quantity);
                }
            }

            if (CartEntries.Any())
            {
                SubTotal = CartEntries
                    .Aggregate(
                    0M,
                    (total, entry) => total + (entry.Value * entry.Key.Price),
                    total => total);
            }
        }
    }

    public CartViewModel(AnonymousCart cart)
    {
        if (cart != null)
        {
            foreach (var entry in cart.CartEntries)
            {
                if (CartEntries.ContainsKey(entry.Product))
                {
                    CartEntries[entry.Product] += entry.Quantity;
                }
                else
                {
                    CartEntries.Add(entry.Product, entry.Quantity);
                }
            }

            if (CartEntries.Any())
            {
                SubTotal = CartEntries
                    .Aggregate(
                    0M,
                    (total, entry) => total + (entry.Value * entry.Key.Price),
                    total => total);
            }
        }
    }
}
