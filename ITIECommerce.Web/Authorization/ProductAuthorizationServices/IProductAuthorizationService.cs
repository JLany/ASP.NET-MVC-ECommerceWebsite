using ITIECommerce.Data.Models;
using ITIECommerce.Web.Models;
using System.Security.Claims;

namespace ITIECommerce.Web.Authorization.ProductAuthorizationServices
{
    public interface IProductAuthorizationService
    {
        Task<bool> AuthorizeCreateAsync(ClaimsPrincipal user);
        Task<bool> AuthorizeDeleteAsync(ClaimsPrincipal user, Product product);
        Task<bool> AuthorizeDeleteAsync(ClaimsPrincipal user, ProductViewModel product);
        Task<bool> AuthorizeUpdateAsync(ClaimsPrincipal user, Product product);
        Task<bool> AuthorizeUpdateAsync(ClaimsPrincipal user, ProductViewModel product);
    }
}