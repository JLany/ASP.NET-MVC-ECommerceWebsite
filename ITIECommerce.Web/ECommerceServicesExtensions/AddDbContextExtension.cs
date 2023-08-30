using ITIECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace ITIECommerce.Web.ECommerceServicesExtensions
{
    public static class AddDbContextExtension
    {
        public static IServiceCollection AddECommerceDbContext(this IServiceCollection services
            , string connectionString)
        {
            services.AddDbContext<ITIECommerceDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
