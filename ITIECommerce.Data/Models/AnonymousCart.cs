using System.ComponentModel.DataAnnotations.Schema;

namespace ITIECommerce.Data.Models;

public class AnonymousCart
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime Created { get; set; } = DateTime.UtcNow;
    
    public virtual ICollection<CartEntry<AnonymousCart>> CartEntries { get; set; }
}
