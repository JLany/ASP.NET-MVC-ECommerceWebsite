using ITIECommerce.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ITIECommerce.Web.Authorization.ProductAuthorizationHandlers;

public class ProductCreateAuthorizationHandler
    : AuthorizationHandler<OperationAuthorizationRequirement, Product>
{
    public ProductCreateAuthorizationHandler()
    {
        
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext handlerContext,
        OperationAuthorizationRequirement requirement,
        Product resource)
    {
        if (handlerContext.User is null)
        {
            return Task.CompletedTask;
        }

        if (handlerContext.User.IsInRole(Constants.ProductSellerRoleName)
            && requirement.Name == Constants.CreateOperationName) 
        {
            handlerContext.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
