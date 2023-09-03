using ITIECommerce.Data;
using ITIECommerce.Data.Models;
using ITIECommerce.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITIECommerce.Web.Controllers;

[AllowAnonymous]
public class CartController : Controller
{
    private static readonly string CartIdKey = "CartId";
    private readonly ITIECommerceDbContext _context;
    private readonly UserManager<ITIECommerceUser> _userManager;
    private readonly ILogger<CartController> _logger;

    public CartController(
        ITIECommerceDbContext context,
        UserManager<ITIECommerceUser> userManager, 
        ILogger<CartController> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
        dynamic cart = GetCartOrCreate();

        if (cart is Cart)
        {
            return View(new CartViewModel(cart as Cart));
        }

        if (cart is AnonymousCart)
        {
            return View(new CartViewModel(cart as AnonymousCart));
        }

        throw new InvalidOperationException();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> AddToCart(CartItemViewModel cartItem)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);

        if (product is null)
        {
            ModelState.AddModelError("ProductNull", $"No Product exists with the provided id = {cartItem.ProductId}.");
            return BadRequest(ModelState);
        }

        if (cartItem.Quantity > product.Quantity || cartItem.Quantity < 1)
        {
            ModelState.AddModelError("NotEnoughQuantity", "There is not enough quantity of the specified product.");
            return BadRequest(ModelState);
        }

        var cart = GetCartOrCreate();

        await AddItemToCartAsync(cart, cartItem);
        await ReduceItemQuantityAsync(cartItem);

        return RedirectToAction("Details", "Products", new { id = cartItem.ProductId });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var cart = GetCartOrCreate();

        int count = await RemoveItemFromCartAsync(cart, id);
        await IncrementItemQuantityAsync(id, count);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private dynamic GetCartOrCreate()
    {
        var userId = _userManager.GetUserId(User);
        var cartId = Request.Cookies[CartIdKey];
        Cart cart;
        AnonymousCart anonymousCart;

        if (userId != null && cartId != null)
        {
            TransferToUserCart(userId, cartId);
            Response.Cookies.Delete(CartIdKey);
        }

        if (userId != null)
        {
            cart = _context.Carts
                .Include(c => c.CartEntries)
                .ThenInclude(ce => ce.Product)
                .FirstOrDefault(c => c.CustomerId == userId)!;

            if (cart == null)
            {
                var insert = new Cart
                {
                    CustomerId = userId
                };

                _context.Carts.Add(insert);
                cart = insert;
            }

            _context.SaveChanges();
            return cart;
        }
        else if (cartId != null)
        {
            // Not authenticated.
            anonymousCart = _context.AnonymousCarts
                .Include(c => c.CartEntries)
                .ThenInclude(ce => ce.Product)
                .FirstOrDefault(c => c.Id == cartId)!;

            return anonymousCart;
        }
        else if (cartId == null)
        {
            var insert = new AnonymousCart();
            _context.AnonymousCarts.Add(insert);
            _context.SaveChanges();

            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(60)
            };

            Response.Cookies.Append(CartIdKey, insert.Id, options);
            anonymousCart = insert;

            return anonymousCart;
        }

        return null!;
    }

    private async Task IncrementItemQuantityAsync(int productId, int count)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (product == null)
        {
            throw new ArgumentNullException();
        }

        product.Quantity += count;
    }

    private async Task<int> RemoveItemFromCartAsync(dynamic cart, int id)
    {
        int output = 0;
        var c = cart as Cart;
        var ac = cart as AnonymousCart;
        if (c != null)
        {
            var entry = await _context.CartEntries
                .FirstOrDefaultAsync(ce => ce.CartId == c.CustomerId && ce.ProductId == id);

            if (entry != null)
            {
                output = entry.Quantity;
                _context.CartEntries.Remove(entry);
            }
        }

        if (ac != null)
        {
            var entry = await _context.AnonymousCartEntries
                .FirstOrDefaultAsync(ce => ce.CartId == ac.Id && ce.ProductId == id);

            if (entry != null)
            {
                output = entry.Quantity;
                _context.AnonymousCartEntries.Remove(entry);
            }
        }

        return output;
    }

    private async Task ReduceItemQuantityAsync(CartItemViewModel cartItem)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);

        // Should not throw.
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        product.Quantity -= cartItem.Quantity;

        _context.Update(product);
        await _context.SaveChangesAsync();
    }

    private async Task AddItemToCartAsync(dynamic cart, CartItemViewModel cartItem)
    {

        if (cart is Cart)
        {
            string customerId = cart.CustomerId;
            var entry = await _context.CartEntries
                .FirstOrDefaultAsync(ce => ce.CartId == customerId && ce.ProductId == cartItem.ProductId);

            if (entry == null)
            {
                entry = new CartEntry<Cart> 
                { 
                    CartId = cart.CustomerId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                };

                _context.CartEntries.Add(entry);
            }
            else
            {
                entry.Quantity += cartItem.Quantity;
                _context.CartEntries.Update(entry);
            }
        }

        if (cart is AnonymousCart)
        {
            string cartId = cart.Id;
            var entry = await _context.AnonymousCartEntries
                .FirstOrDefaultAsync(ce => ce.CartId == cartId && ce.ProductId == cartItem.ProductId);

            if (entry == null)
            {
                entry = new CartEntry<AnonymousCart>
                {
                    CartId = cart.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                };

                _context.AnonymousCartEntries.Add(entry);
            }
            else
            {
                entry.Quantity += cartItem.Quantity;
                _context.AnonymousCartEntries.Update(entry);
            }
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Transfers cart entries from <see cref="AnonymousCart"/>
    /// with the specified id to a <see cref="Cart"/> for the specified user.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cartId"></param>
    private void TransferToUserCart(string userId, string cartId)
    {
        var anonymousCart = _context.AnonymousCarts
            .Include(c => c.CartEntries)
            .ThenInclude(ce => ce.Product)
            .First(c => c.Id == cartId);

        var cart = new Cart
        {
            CustomerId = userId,
            CartEntries = new List<CartEntry<Cart>>()
        };

        _context.Carts.Add(cart);

        foreach (var entry in anonymousCart.CartEntries )
        {
            cart.CartEntries?.Add(new CartEntry<Cart>
            {
                CartId = cart.CustomerId,
                Product = entry.Product,
                Quantity = entry.Quantity,
                SubTotal = entry.SubTotal,
                ProductId = entry.ProductId
            });
        }

        _context.AnonymousCarts.Remove(anonymousCart);

        _context.CartEntries.AddRange(cart.CartEntries);

        _context.SaveChanges();
    }
}
