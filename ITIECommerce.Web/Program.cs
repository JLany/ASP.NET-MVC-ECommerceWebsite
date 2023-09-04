using ITIECommerce.Web.ServicesExtensions;
using ITIECommerce.Web.Utility;

namespace ITIECommerce.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.SetupInfrastructureServices();

        builder.Services.RegisterServicesWithDependencyInjection();

        // Configure services options
        builder.ConfigureServicesOptions();

        var app = builder.Build();

        await app.SeedInitialDataAsync();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Products}/{action=Index}/{id?}");

        app.Run();
    }
}