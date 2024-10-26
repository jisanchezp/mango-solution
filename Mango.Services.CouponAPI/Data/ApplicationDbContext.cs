using Microsoft.EntityFrameworkCore;
using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Mango.Services.CouponAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { CouponId = 1, CouponName = "MANGOINFLUENCER#10", DiscountAmount = 10, MinAmount = 20 },
                new Coupon { CouponId = 2, CouponName = "MANGOINFLUENCER#20", DiscountAmount = 20, MinAmount = 35 }
                );
        }
    }
}
