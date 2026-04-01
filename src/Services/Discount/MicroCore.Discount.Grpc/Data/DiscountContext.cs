using MicroCore.Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroCore.Discount.Grpc.Data;


public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    public DiscountContext(DbContextOptions<DiscountContext> options)
       : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}