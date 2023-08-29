using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ITIECommerce.Data.Models;

public class ITIECommerceUser : IdentityUser
{
    [MinLength(5)]
    [MaxLength(200)]
    public string Address { get; set; } = null!;

    public string RoleId { get; set; } = null!;
    public IdentityRole Role { get; set; } = null!;
}
