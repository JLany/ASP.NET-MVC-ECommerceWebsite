namespace ITIECommerce.Data.Models;

public class Cart
{
    public string CustomerId { get; set; }
    public virtual ITIECommerceUser Customer { get; set; }

    public virtual ICollection<CartEntry<Cart>> CartEntries { get; set; }
}
