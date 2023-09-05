using ITIECommerce.Web.Authorization.OrderAuthorizationServices;
using ITIECommerce.Web.Authorization.ProductAuthorizationHandlers;
using ITIECommerce.Web.Authorization.ProductAuthorizationServices;
using ITIECommerce.Web.Utility;
using Microsoft.AspNetCore.Authorization;

namespace ITIECommerce.Web.ServicesExtensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServicesWithDependencyInjection(
            this IServiceCollection services)
        {
            // Product Authorization Services.
            services
                .AddScoped<IProductAuthorizationService, ProductAuthorizationService>()
                .AddScoped<IAuthorizationHandler, ProductIsSellerAuthorizationHandler>()
                .AddScoped<IAuthorizationHandler, ProductCreateAuthorizationHandler>();

            // Order Authorization Services.
            services
                .AddScoped<IOrderAuthorizationService, OrderAuthorizationService>();

            services
                .AddTransient<IImageWriter, ImageWriter>();

            return services;
        }
    }
}
