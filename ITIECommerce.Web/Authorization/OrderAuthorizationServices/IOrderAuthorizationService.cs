using ITIECommerce.Data.Models;
using ITIECommerce.Web.Models;
using System.Security.Claims;

namespace ITIECommerce.Web.Authorization.OrderAuthorizationServices
{
    public interface IOrderAuthorizationService
    {
        Task<bool> AuthorizeCreateAsync(ClaimsPrincipal user, Order order);
        Task<bool> AuthorizeCreateAsync(ClaimsPrincipal user, OrderViewModel order);
        Task<bool> AuthorizeDeleteAsync(ClaimsPrincipal user, Order order);
        Task<bool> AuthorizeDeleteAsync(ClaimsPrincipal user, OrderViewModel order);
        Task<bool> AuthorizeReadAsync(ClaimsPrincipal user, Order order);
        Task<bool> AuthorizeUpdateAsync(ClaimsPrincipal user, Order order);
        Task<bool> AuthorizeUpdateAsync(ClaimsPrincipal user, OrderViewModel order);
    }
}