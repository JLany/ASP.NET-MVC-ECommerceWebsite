using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ITIECommerce.Web.Models;

public class RegisterViewModel
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

    [MinLength(5)]
    [MaxLength(200)]
    public string Address { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    [MinLength(2)]
    public string? FirstName { get; set; }

    [MinLength(2)]
    public string? LastName { get; set; }

    public bool SignUpAsSeller { get; set; } = false;
}
