using Microsoft.AspNetCore.Identity;

namespace ITIECommerce.Web.ServicesExtensions
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
