﻿using ITIECommerce.Web.Authorization.ProductAuthorizationHandlers;
using ITIECommerce.Web.Authorization.ProductAuthorizationServices;
using ITIECommerce.Web.Utility;
using Microsoft.AspNetCore.Authorization;

namespace ITIECommerce.Web.ECommerceServicesExtensions
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

            services
                .AddTransient<IImageWriter, ImageWriter>();

            

            return services;
        }
    }
}
