namespace ITIECommerce.Web.ServicesExtensions
{
    public static class ConfigureCookieExtension
    {
        public static IServiceCollection ConfigureCookieOptions(this IServiceCollection services)
        {
            return services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings.
                options.Cookie.HttpOnly = true;

                options.ExpireTimeSpan = TimeSpan.FromHours(24);

                options.LoginPath = "/Accounts/Register";
                options.AccessDeniedPath = "/Accounts/AccessDenied";
                options.SlidingExpiration = true;
            });
        }
    }
}
