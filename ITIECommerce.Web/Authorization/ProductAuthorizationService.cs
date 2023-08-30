using ITIECommerce.Data.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ITIECommerce.Web.Authorization;

public class ProductAuthorizationService : IProductAuthorizationService
{
    private readonly IAuthorizationService _authorizationService;

    public ProductAuthorizationService(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, Product product, ProductOperation operation)
    {
        throw new NotImplementedException();
    }
}
