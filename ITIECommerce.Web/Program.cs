using ITIECommerce.Web.ServicesExtensions;
using ITIECommerce.Web.Utility;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;

namespace ITIECommerce.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.SetupInfrastructureServices();

        builder.Services.RegisterServicesWithDependencyInjection();

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.Cookie.Name = "ITI_Ecommrece_Cookie";
            options.Cookie.IsEssential = true;
            options.Cookie.HttpOnly = false;

            options.IdleTimeout = TimeSpan.FromSeconds(30);
        });

        // Configure services options
        builder.ConfigureServicesOptions();

        var app = builder.Build();

        await app.SeedInitialDataAsync();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        // app.UseHttpsRedirection();

        // Forwarded Headers Middleware should run before other middleware.
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Products}/{action=Index}/{id?}"
            );

        app.Run();
    }
}