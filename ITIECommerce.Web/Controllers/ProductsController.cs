using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITIECommerce.Data;
using ITIECommerce.Data.Models;
using Microsoft.AspNetCore.Authorization;
using ITIECommerce.Web.Models;
using Microsoft.AspNetCore.Identity;
using ITIECommerce.Web.Authorization.ProductAuthorizationServices;
using ITIECommerce.Web.Utility;
using System.Xml.Linq;

namespace ITIECommerce.Web.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly string ProductImagesUploadPath = "/upload/images/products";
        private readonly ITIECommerceDbContext _context;
        private readonly UserManager<ITIECommerceUser> _userManager;
        private readonly IProductAuthorizationService _authorizationService;
        private readonly IImageWriter _imageWriter;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            ITIECommerceDbContext context,
            UserManager<ITIECommerceUser> userManager,
            IProductAuthorizationService authorizationService,
            IImageWriter imageWriter,
            ILogger<ProductsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
            _imageWriter = imageWriter;
            _logger = logger;
        }

        // GET: Products
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var products = _context.Products.Include(p => p.Seller);
            return View(
                await products
                .Select(p => new ProductViewModel(p))
                .ToListAsync());
        }
            
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> MyProducts()
        {
            var userId = _userManager.GetUserId(User);
            var products = _context.Products
                .Where(p => p.SellerId == userId);

            return 
                View(await products
                .Select(p => new ProductViewModel(p))
                .ToListAsync());
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(new ProductViewModel(product));
        }

        // GET: Products/Create
        [Authorize(Roles = "Seller")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,Description,Price,Quantity,Image")] ProductViewModel product)
        {
            bool isAuthorized = await _authorizationService
                .AuthorizeCreateAsync(User);

            if (!isAuthorized)
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var userId = _userManager.GetUserId(User);

            product.SellerId = userId;
            product.ImageUri = await _imageWriter
                .WriteImageToRootAsync(product.Image, ProductImagesUploadPath);

            //SellerId = viewModel.SellerId;
            //Name = viewModel.Name;
            //Description = viewModel.Description;
            //Price = viewModel.Price;
            //Quantity = viewModel.Quantity;
            //ImageUri = viewModel.ImageUri;

            _context.Add(new Product
            {
                SellerId = product.SellerId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                ImageUri = product.ImageUri,
            });
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyProducts));
        }

        // GET: Products/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            bool isAuthorized = await _authorizationService
                .AuthorizeUpdateAsync(User, product);

            if (!isAuthorized)
            {
                return Forbid();
            }

            return View(new ProductViewModel(product));
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,SellerId,Name,Description,Price,Quantity,ImageUri")] ProductViewModel product)
        {
            bool isAuthorized = await _authorizationService
                .AuthorizeUpdateAsync(User, product);

            if (!isAuthorized)
            {
                return Forbid();
            }

            if (id != product.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var updateCandidate = await _context.Products
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(p => p.Id ==  product.Id);

            if (updateCandidate == null)
            {
                return NotFound();
            }

            await UpdateProductAsync(updateCandidate, product);

            _context.Update(updateCandidate);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyProducts));
        }

        /// <summary>
        /// Updates the <see cref="Product"/>'s properties with the properties of <see cref="ProductViewModel"/>.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private async Task UpdateProductAsync(Product product, ProductViewModel viewModel)
        {
            product.Name = viewModel.Name;
            product.Description = viewModel.Description;
            product.Price = viewModel.Price;
            product.Quantity = viewModel.Quantity;

            string? imageUri = await _imageWriter
                .WriteImageToRootAsync(viewModel.Image, ProductImagesUploadPath);

            if (imageUri != null)
            {
                product.ImageUri = imageUri;
            }
        }

        // DELETE: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ITIECommerceDbContext.Products'  is null.");
            }

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            
            if (product == null)
            {
                return NotFound(); 
            }

            bool isAuthorized = await _authorizationService
                .AuthorizeDeleteAsync(User, product);
            
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyProducts));
        }

        private bool ProductExists(int? id)
        {
          return (_context.Products?
                .Any(e => e.Id == (id ?? 0)))
                .GetValueOrDefault();
        }
    }
}
