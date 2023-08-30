using Microsoft.AspNetCore.Identity;

namespace ITIECommerce.Web.ECommerceServicesExtensions
{
    public static class ConfigureServicesOptionsExtension
    {
        public static void ConfigureServicesOptions(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureIdentityOptions<IdentityOptions>();
            builder.Services.ConfigureCookieOptions();
        }
    }
}
