namespace ITIECommerce.Web.ECommerceServicesExtensions
{
    public static class AddServicesExtension
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("ITIECommerceDB")
                ?? throw new InvalidOperationException("Connection string for 'ITIECommerceDB' not found.");

            builder.Services.AddECommerceDbContext(connectionString);

            builder.Services.AddControllersWithViews();
        }
    }
}
