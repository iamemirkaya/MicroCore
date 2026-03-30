using MicroCore.Basket.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MicroCore.Basket.Api.Data;

public class BasketDbContext : DbContext
{
    public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options)
    {
    }

    public DbSet<MicroCore.Basket.Api.Models.Basket> Baskets { get; set; }
    public DbSet<MicroCore.Basket.Api.Models.BasketItem> BasketItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MicroCore.Basket.Api.Models.Basket>()
        .HasKey(b => b.UserId);

        modelBuilder.Entity<MicroCore.Basket.Api.Models.Basket>().Ignore(b => b.IsApplyDiscount);
        modelBuilder.Entity<MicroCore.Basket.Api.Models.Basket>().Ignore(b => b.TotalPrice);
        modelBuilder.Entity<MicroCore.Basket.Api.Models.Basket>().Ignore(b => b.TotalPriceWithAppliedDiscount);



        modelBuilder.Entity<BasketItem>()
            .Property(bi => bi.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<BasketItem>()
            .Property(bi => bi.PriceByApplyDiscountRate)
            .HasColumnType("decimal(18,2)");
    }
}