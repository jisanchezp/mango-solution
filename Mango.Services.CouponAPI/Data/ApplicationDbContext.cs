using Microsoft.EntityFrameworkCore;
using Mango.Services.CouponAPI.Models;

namespace Mango.Services.CouponAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {            
        }
    }
}
