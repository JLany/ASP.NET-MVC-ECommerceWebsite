using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ITIECommerce.Web.Models;

public class RegisterViewModel
{
    [DataType(DataType.EmailAddress)]
    public string UserName { get; set; }

    [DataType(DataType.Password)]
    [Remote(action: "IsStrongPassword", controller: "Accounts")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

    [MinLength(5)]
    [MaxLength(200)]
    public string Address { get; set; }

    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
