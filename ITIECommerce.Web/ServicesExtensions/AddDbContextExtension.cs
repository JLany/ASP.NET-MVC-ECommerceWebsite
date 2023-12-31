﻿using ITIECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace ITIECommerce.Web.ServicesExtensions
{
    public static class AddDbContextExtension
    {
        public static IServiceCollection AddITIECommerceDbContext(this IServiceCollection services
            , string connectionString)
        {
            services.AddDbContext<ITIECommerceDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
