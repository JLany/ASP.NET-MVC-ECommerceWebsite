using ITIECommerce.Data.Models;
using ITIECommerce.Web.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ITIECommerce.Web.Authorization.OrderAuthorizationServices;

public class OrderAuthorizationService : IOrderAuthorizationService
{
    private static class OrderOperations
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

    public OrderAuthorizationService(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task<bool> AuthorizeCreateAsync(ClaimsPrincipal user, Order order)
    {
        var authResult = await _authorizationService
            .AuthorizeAsync(user, order, OrderOperations.Create);
        return authResult.Succeeded;
    }

    public async Task<bool> AuthorizeCreateAsync(ClaimsPrincipal user, OrderViewModel order)
    {
        return await AuthorizeCreateAsync(user, new Order(order));
    }

    public async Task<bool> AuthorizeUpdateAsync(ClaimsPrincipal user, Order order)
    {
        var authResult = await _authorizationService
            .AuthorizeAsync(user, order, OrderOperations.Update);
        return authResult.Succeeded;
    }

    public async Task<bool> AuthorizeUpdateAsync(ClaimsPrincipal user, OrderViewModel order)
    {
        return await AuthorizeUpdateAsync(user, new Order(order));
    }

    public async Task<bool> AuthorizeDeleteAsync(ClaimsPrincipal user, Order order)
    {
        var authResult = await _authorizationService
            .AuthorizeAsync(user, order, OrderOperations.Delete);
        return authResult.Succeeded;
    }

    public async Task<bool> AuthorizeDeleteAsync(ClaimsPrincipal user, OrderViewModel order)
    {
        return await AuthorizeDeleteAsync(user, new Order(order));
    }

    public async Task<bool> AuthorizeReadAsync(ClaimsPrincipal user, Order order)
    {
        var authResult = await _authorizationService
            .AuthorizeAsync(user, order, OrderOperations.Read);
        return authResult.Succeeded;
    }
}
