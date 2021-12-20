using Microsoft.EntityFrameworkCore;
using Payment.Core.Entities;
using Payment.Data.Context.Configurations;

namespace Payment.Data.Context
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options) {}
        public DbSet<BinNumber> BinNumbers { get; set; }
        public DbSet<PaymentActivity> paymentActivities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BinNumberConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentActivityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
