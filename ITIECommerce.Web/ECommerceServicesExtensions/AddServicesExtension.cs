using ITIECommerce.Data;
using ITIECommerce.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ITIECommerce.Web.ECommerceServicesExtensions
{
    public static class AddServicesExtension
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            // Add DbContext.
            var connectionString = builder.Configuration.GetConnectionString("ITIECommerceDB")
                ?? throw new InvalidOperationException("Connection string for 'ITIECommerceDB' not found.");

            builder.Services.AddECommerceDbContext(connectionString);

            // Configure Identity, Authentication and Cookie.
            builder.Services.AddITIECommerceIdentity<ITIECommerceUser>(
                options => options.SignIn.RequireConfirmedEmail = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ITIECommerceDbContext>()
                .AddSignInManager();

            // Add Authorization.
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            builder.Services.RegisterServicesWithDependencyInjection();

            // Add MVC.
            builder.Services.AddControllersWithViews();
        }
    }
}
