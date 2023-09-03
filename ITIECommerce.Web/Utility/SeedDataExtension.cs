using ITIECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace ITIECommerce.Web.Utility;

public static class SeedDataExtension
{
    public static async Task SeedInitialDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ITIECommerceDbContext>();

        await context.Database.MigrateAsync();

        await SeedData.InitializeAsync(services);
    }
}
