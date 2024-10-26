using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CouponController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Coupon> coupons = new List<Coupon>();

            try
            {
                coupons = _db.Coupons.ToList();

                return Ok(coupons);
            }
            catch (Exception ex)
            {
                return BadRequest(coupons);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            Coupon? coupon;

            try
            {
                coupon = _db.Coupons.FirstOrDefault(c => c.CouponId == id);

                if (coupon == null)
                {
                    return NotFound();
                }

                return Ok(coupon);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
