using ITIECommerce.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ITIECommerce.Web.ServicesExtensions
{
    public static class AddDbContextExtension
    {
        public static IServiceCollection AddITIECommerceDbContext(this IServiceCollection services
            , string connectionString)
        {
            services.AddDbContext<ITIECommerceDbContext>(options =>
                // options.UseSqlServer(connectionString));
                options.UseSqlite(connectionString));

            var connection = new SqliteConnection(connectionString);
            connection.CreateFunction("newid", () => Guid.NewGuid());

            return services;
        }
    }
}
