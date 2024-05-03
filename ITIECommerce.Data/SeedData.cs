using ITIECommerce.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;

namespace ITIECommerce.Data;

public class SeedData
{
    private static IServiceProvider _serviceProvider;
    private static string _sellerId;
    private static string _customerId;

    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        using var context = serviceProvider.GetRequiredService<ITIECommerceDbContext>();

        _sellerId = await EnsureUser(serviceProvider, "seller@Ecommerce1.com", "seller@Ecommerce1.com");
        await EnsureRole(serviceProvider, _sellerId, "Seller");

        // allowed user can create and edit contacts that they create
        _customerId = await EnsureUser(serviceProvider, "customer@Ecommerce1.com", "customer@Ecommerce1.com");
        await EnsureRole(serviceProvider, _customerId, "Customer");

        await SeedDB(context);
    }

    private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
        string password, string userName)
    {
        var userManager = serviceProvider.GetService<UserManager<ITIECommerceUser>>();

        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            user = new ITIECommerceUser
            {
                UserName = userName,
                EmailConfirmed = true,
                FirstName = userName,
                Address = "30 Ard El Gammal",
            };

            var result = await userManager.CreateAsync(user, password);
        }

        if (user == null)
        {
            throw new Exception("The password is probably not strong enough!");
        }

        return user.Id;
    }

    private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
        string uid, string role)
    {
        var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>()
            ?? throw new NotSupportedException("roleManager null");

        IdentityResult result;
        if (!await roleManager.RoleExistsAsync(role))
        {
            result = await roleManager.CreateAsync(new IdentityRole(role));
        }

        var userManager = serviceProvider.GetService<UserManager<ITIECommerceUser>>();

        //if (userManager == null)
        //{
        //    throw new Exception("userManager is null");
        //}

        var user = await userManager.FindByIdAsync(uid)
            ?? throw new Exception("The testUserPw password was probably not strong enough!");

        result = await userManager.AddToRoleAsync(user, role);

        return result;
    }

    public static async Task SeedDB(ITIECommerceDbContext context)
    {
        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product
                {
                    Name = "Chocolate",
                    SellerId = _sellerId,
                    Description = "This is delicious.",
                    Price = 15.99M,
                    Quantity = 20,
                    ImageUri = "/images/product/product1.jpg",
                },
                new Product
                {
                    Name = "Cookies",
                    SellerId = _sellerId,
                    Description = "This is delicious.",
                    Price = 1.99M,
                    Quantity = 20,
                    ImageUri = "/images/product/product5.jpg",
                },
                new Product
                {
                    Name = "NIKE Sneakers",
                    SellerId = _sellerId,
                    Description = "Comfy sports ware.",
                    Price = 85.99M,
                    Quantity = 20,
                    ImageUri = "/images/product/product2.jpg",
                },
                new Product
                {
                    Name = "T-Shirt",
                    SellerId = _sellerId,
                    Description = "Classic for the boomers.",
                    Price = 12.99M,
                    Quantity = 20,
                    ImageUri = "/images/product/product3.jpg",
                },
                new Product
                {
                    Name = "Car",
                    SellerId = _sellerId,
                    Description = "We sell cars, too.",
                    Price = 25400.00M,
                    Quantity = 20,
                    ImageUri = "/images/product/product4.jpg",
                }
             );
        }

        if (!context.Orders.Any())
        {
            await context.Orders.AddRangeAsync(
                new Order
                {
                    CustomerId = _customerId,
                    Address = "30 Ard El Gammal",
                    Total = 514M,
                },
                new Order
                {
                    CustomerId = _customerId,
                    Address = "30 Ard El Gammal",
                    Total = 120M,
                }
            );
        }

        await context.SaveChangesAsync();

        if (!context.OrderEntries.Any())
        {

            await context.OrderEntries.AddRangeAsync(
                new OrderEntry
                {
                    OrderId = 1,
                    ProductId = 1,
                    Quantity = 5,
                    SubTotal = 79.95M,
                },
                new OrderEntry
                {
                    OrderId = 1,
                    ProductId = 2,
                    Quantity = 1,
                    SubTotal = 1.99M,
                },
                new OrderEntry
                {
                    OrderId = 2,
                    ProductId = 5,
                    Quantity = 1,
                    SubTotal = 25400.00M,
                },
                new OrderEntry
                {
                    OrderId = 2,
                    ProductId = 3,
                    Quantity = 1,
                    SubTotal = 85.99M,
                }
            );
        }

        await context.SaveChangesAsync();
    }
}
