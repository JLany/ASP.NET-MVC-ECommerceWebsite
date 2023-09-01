using ITIECommerce.Data.Models;
using ITIECommerce.Web.Authorization.ProductAuthorizationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace ITIECommerce.Web.Authorization.ProductAuthorizationHandlers;

/// <summary>
/// Authorize a user to perform operations on his own sale produtcs.
/// </summary>
public class ProductIsSellerAuthorizationHandler 
    : AuthorizationHandler<OperationAuthorizationRequirement, Product>
{
    private readonly UserManager<ITIECommerceUser> _userManager;

    public ProductIsSellerAuthorizationHandler(
        UserManager<ITIECommerceUser> userManager)
    {
        _userManager = userManager;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext handlerContext,
        OperationAuthorizationRequirement requirement,
        Product resource)
    {
        if (handlerContext.User is null || resource == null)
        {
            return Task.CompletedTask;
        }

        // If not asking for CRUD operations, fail.
        if (requirement.Name != Constants.CreateOperationName &&
            requirement.Name != Constants.ReadOperationName &&
            requirement.Name != Constants.UpdateOperationName &&
            requirement.Name != Constants.DeleteOperationName)
        {
            return Task.CompletedTask;
        }

        var userId = _userManager.GetUserId(handlerContext.User);

        if (userId == resource.SellerId)
        {
            handlerContext.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
