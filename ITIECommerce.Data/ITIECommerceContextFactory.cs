using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ITIECommerce.Data;

/// <summary>
/// This class is responsible for exposing a DbContext factory for the migration command.
/// </summary>
internal class ITIECommerceContextFactory : IDesignTimeDbContextFactory<ITIECommerceDbContext>
{
    public ITIECommerceDbContext CreateDbContext(string[] args)
    {
        var dbContextBuilder = Activator
            .CreateInstance<DbContextOptionsBuilder<ITIECommerceDbContext>>();

        //var connectionString = new ConfigurationBuilder()
        //    .SetBasePath(
        //    Directory.GetParent(".")!
        //    .GetDirectories()
        //    .First(d => d.Name == "ITIECommerce.Web")
        //    .FullName)
        //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
        //    .AddEnvironmentVariables()
        //    .Build()
        //    .GetConnectionString("ITIECommerceDB");

        var connectionString = "Server=ymelk-iti-ecommerce-server.postgres.database.azure.com;Database=iti-ecommerce-database;Port=5432;Ssl Mode=VerifyFull;User Id=dtisxdkygi;Password=UW3SM56QKTYMG311$;";

        dbContextBuilder.UseNpgsql(connectionString);

        return new ITIECommerceDbContext(dbContextBuilder.Options);
    }
}
