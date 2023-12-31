﻿using Microsoft.AspNetCore.Identity;

namespace ITIECommerce.Web.ServicesExtensions
{
    public static class AddIdentityExtension
    {
        public static IdentityBuilder AddITIECommerceIdentity<TUser>(this IServiceCollection services
            , Action<IdentityOptions> configureOptions) where TUser : IdentityUser
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies(options => { });

            return services
                .AddIdentityCore<TUser>(options =>
                {
                    options.Stores.MaxLengthForKeys = 128;
                    configureOptions?.Invoke(options);
                })
                .AddDefaultTokenProviders();
        }
    }
}
