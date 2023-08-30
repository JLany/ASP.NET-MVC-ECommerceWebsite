using ITIECommerce.Web.Authorization;

namespace ITIECommerce.Web.ECommerceServicesExtensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServicesWithDependencyInjection(
            this IServiceCollection services)
        {
            return services
                .AddScoped<IProductAuthorizationService, ProductAuthorizationService>();
        }
    }
}
