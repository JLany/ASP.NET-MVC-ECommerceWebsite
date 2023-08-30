namespace ITIECommerce.Web.ECommerceServicesExtensions
{
    public static class ConfigureCookieExtension
    {
        public static IServiceCollection ConfigureCookieOptions(this IServiceCollection services)
        {
            return services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings.
                options.Cookie.HttpOnly = true;

                // TODO: Replace with more forgiving time span.
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Accounts/Login";
                options.AccessDeniedPath = "/Accounts/AccessDenied";
                options.SlidingExpiration = true;
            });
        }
    }
}
