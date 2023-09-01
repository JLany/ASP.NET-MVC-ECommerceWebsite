using ITIECommerce.Data.Models;
using ITIECommerce.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITIECommerce.Web.Controllers;

// TODO: Add the Manage Action.

[Route("/Accounts/{action=Login}")]
public class AccountsController : Controller
{
    private readonly UserManager<ITIECommerceUser> _userManager;
    private readonly IUserStore<ITIECommerceUser> _userStore;
    private readonly IUserEmailStore<ITIECommerceUser> _emailStore;
    private readonly IUserRoleStore<ITIECommerceUser> _roleStore;
    private readonly SignInManager<ITIECommerceUser> _signInManager;
    private readonly ILogger<AccountsController> _logger;

    public AccountsController(
        UserManager<ITIECommerceUser> userManager,
        IUserStore<ITIECommerceUser> userStore,
        SignInManager<ITIECommerceUser> signInManager,
        ILogger<AccountsController> logger
        /*, IEmailSender emailSender*/)
    {
        _userManager = userManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
        _roleStore = GetRoleStore();
        _signInManager = signInManager;
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        var result = await _signInManager
            .PasswordSignInAsync(login.UserName,
            login.Password,
            isPersistent: login.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            _logger.LogInformation("User logged in.");
            return RedirectToAction("Index", "Products");
        }

        return View(login);

    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out.");

        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (ModelState.IsValid)
        {
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, registerViewModel.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, registerViewModel.Email, CancellationToken.None);
            await _roleStore.AddToRoleAsync(user, "Customer", CancellationToken.None);

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            var userId = await _userManager.GetUserIdAsync(user);

            if (result.Succeeded)
            {
                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Products", userId);
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // If we reach here, there is something wrong with the registeration.
        return View(registerViewModel);
    }

    private ITIECommerceUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<ITIECommerceUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ITIECommerceUser)}'. " +
                $"Ensure that '{nameof(ITIECommerceUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }

    private IUserRoleStore<ITIECommerceUser> GetRoleStore()
    {
        if (!_userManager.SupportsUserRole)
        {
            throw new NotSupportedException(
                "The Account controller requires a user manager with user role support.");
        }

        return (_userStore as IUserRoleStore<ITIECommerceUser>)!;
    }

    private IUserEmailStore<ITIECommerceUser> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException(
                "The Account controller requires a user manager with email support.");
        }

        return (_userStore as IUserEmailStore<ITIECommerceUser>)!;
    }
}
