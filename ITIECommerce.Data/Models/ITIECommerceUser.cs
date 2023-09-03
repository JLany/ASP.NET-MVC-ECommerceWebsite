using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ITIECommerce.Data.Models;

public class ITIECommerceUser : IdentityUser
{
    [MinLength(5)]
    [MaxLength(200)]
    public string Address { get; set; } = null!;

    public string? FirstName { get; set; }
    public string? LastName { get; set;}

    public virtual Cart Cart { get; set; }

    public virtual ICollection<Product>? Products { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}
