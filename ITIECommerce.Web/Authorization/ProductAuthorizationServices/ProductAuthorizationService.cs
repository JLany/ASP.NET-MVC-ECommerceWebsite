using ITIECommerce.Data.Models;
using ITIECommerce.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace ITIECommerce.Web.Authorization.ProductAuthorizationServices;

public class ProductAuthorizationService : IProductAuthorizationService
{
    private static class ProductOperations
    {
        public static readonly OperationAuthorizationRequirement Read =
            new() { Name = Constants.ReadOperationName };

        public static readonly OperationAuthorizationRequirement Delete =
           new() { Name = Constants.DeleteOperationName };

        public static readonly OperationAuthorizationRequirement Update =
           new() { Name = Constants.UpdateOperationName };

        public static readonly OperationAuthorizationRequirement Create =
           new() { Name = Constants.CreateOperationName };
    }

    private readonly IAuthorizationService _authorizationService;

    public ProductAuthorizationService(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task<bool> AuthorizeCreateAsync(ClaimsPrincipal user)
    {
        var authResult = await _authorizationService
            .AuthorizeAsync(user, new Product(), ProductOperations.Create);
        return authResult.Succeeded;
    }

    public async Task<bool> AuthorizeUpdateAsync(ClaimsPrincipal user, Product product)
    {
        var authResult = await _authorizationService
            .AuthorizeAsync(user, product, ProductOperations.Update);
        return authResult.Succeeded;
    }

    public async Task<bool> AuthorizeUpdateAsync(ClaimsPrincipal user, ProductViewModel product)
    {
        return await AuthorizeUpdateAsync(user, new Product(product));
    }

    public async Task<bool> AuthorizeDeleteAsync(ClaimsPrincipal user, Product product)
    {
        var authResult = await _authorizationService
            .AuthorizeAsync(user, product, ProductOperations.Delete);
        return authResult.Succeeded;
    }

    public async Task<bool> AuthorizeDeleteAsync(ClaimsPrincipal user, ProductViewModel product)
    {
        return await AuthorizeDeleteAsync(user, new Product(product));
    }
}
