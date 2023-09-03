﻿using ITIECommerce.Data.Models;
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

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
    }
}
