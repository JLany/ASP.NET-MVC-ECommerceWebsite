using ITIECommerce.Data.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ITIECommerce.Web.Authorization
{
    public interface IProductAuthorizationService
    {
        Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, Product product, ProductOperation operation);
    }
}