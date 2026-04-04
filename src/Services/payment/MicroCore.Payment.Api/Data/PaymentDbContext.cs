using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MicroCore.Payment.Api.Data
{
    public class PaymentDbContext : DbContext
    {

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }
        public DbSet<Payment.Api.Models.Payment> Courses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
