using ITIECommerce.Data.Interceptors;
using ITIECommerce.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITIECommerce.Data;

public class ITIECommerceDbContext : IdentityDbContext<ITIECommerceUser>
{
    public ITIECommerceDbContext(DbContextOptions<ITIECommerceDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderEntry> OrderEntries { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!; 
    public DbSet<CartEntry<Cart>> CartEntries { get; set; } = null!;
    public DbSet<AnonymousCart> AnonymousCarts { get; set; } = null!;
    public DbSet<CartEntry<AnonymousCart>> AnonymousCartEntries { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Cart>()
            .HasKey(c => c.CustomerId);

        #region Column Types
        builder.Entity<Product>()
            .Property("Price")
            .HasColumnType("money");

        builder.Entity<Order>()
            .Property("Total")
            .HasColumnType("money");

        builder.Entity<Order>()
            .Property("ShippingCost")
            .HasColumnType("money");

        builder.Entity<Order>()
            .Property("SubTotal")
            .HasColumnType("money");

        builder.Entity<OrderEntry>()
            .Property("SubTotal")
            .HasColumnType("money");

        builder.Entity<CartEntry<Cart>>()
            .Property(ce => ce.SubTotal)
            .HasColumnType("money");

        builder.Entity<CartEntry<AnonymousCart>>()
            .Property(ce => ce.SubTotal)
            .HasColumnType("money");

        builder.Entity<ITIECommerceUser>()
            .Property(u => u.Id)
            .HasColumnType("nvarchar(128)");

        builder.Entity<Cart>()
            .Property("CustomerId")
            .HasColumnType("nvarchar(128)");

        builder.Entity<AnonymousCart>()
            .Property(AnonymousCart => AnonymousCart.Id)
            .HasColumnType("nvarchar(128)");
        #endregion

        #region Default Values
        builder.Entity<Order>()
            .Property(o => o.CreateDate)
            .HasDefaultValueSql("DATE('now')");

        builder.Entity<AnonymousCart>()
            .Property(AnonymousCart => AnonymousCart.Id)
            .HasDefaultValueSql("newid()");
        #endregion

        #region Query Filters
        builder.Entity<Product>()
            .HasQueryFilter(p => p.IsDeleted == false);

        builder.Entity<CartEntry<Cart>>()
            .HasQueryFilter(ce => ce.Product.IsDeleted == false);

        builder.Entity<CartEntry<AnonymousCart>>()
            .HasQueryFilter(ce => ce.Product.IsDeleted == false);

        builder.Entity<OrderEntry>() 
            .HasQueryFilter(oe => oe.Product.IsDeleted == false);
        #endregion
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder
            .AddInterceptors(new SoftDeleteInterceptor());
    }
}
