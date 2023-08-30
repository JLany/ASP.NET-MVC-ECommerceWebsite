using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ITIECommerce.Data;

internal class ITIECommerceContextFactory : IDesignTimeDbContextFactory<ITIECommerceDbContext>
{
    public ITIECommerceDbContext CreateDbContext(string[] args)
    {
        var dbContextBuilder = Activator
            .CreateInstance<DbContextOptionsBuilder<ITIECommerceDbContext>>();

        var connectionString = new ConfigurationBuilder()
            .SetBasePath(
            Directory.GetParent(".")!
            .GetDirectories()
            .First(d => d.Name == "ITIECommerce.Web")
            .FullName)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build()
            .GetConnectionString("ITIECommerceDB");

        dbContextBuilder.UseSqlServer(connectionString);

        return new ITIECommerceDbContext(dbContextBuilder.Options);
    }
}
