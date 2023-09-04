using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITIECommerce.Data;
using ITIECommerce.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ITIECommerce.Web.Models;
using ITIECommerce.Web.Authorization.OrderAuthorizationServices;

namespace ITIECommerce.Web.Controllers;

[Authorize(Roles = "Customer")]
public class OrdersController : Controller
{
    private readonly ITIECommerceDbContext _context;
    private readonly UserManager<ITIECommerceUser> _userManager;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(
        ITIECommerceDbContext context,
        UserManager<ITIECommerceUser> userManager,
        ILogger<OrdersController> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    // GET: Orders
    public IActionResult Index()
    {
        var userId = _userManager.GetUserId(User);

        if (userId is null)
        {
            return Forbid();
        }

        var orders = _context.Orders
            .Where(o => o.CustomerId == userId);

        return View(orders
            .AsParallel()
            .Select(o => new OrderViewModel(o))
            .ToList());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Orders == null)
        {
            return NotFound();
        }

        var order = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderEntries)!
            .ThenInclude(oe => oe.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        var userId = _userManager.GetUserId(User);
        bool isAuthorized = userId == order.CustomerId;

        if (!isAuthorized)
        {
            return Forbid();
        }

        return View(new OrderViewModel(order));
    }
}
