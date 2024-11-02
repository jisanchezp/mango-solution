using Mango.Frontend.MVC.Helper;
using Mango.Frontend.MVC.Models;
using Mango.Frontend.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Mango.Frontend.MVC.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> Index()
        {
            List<CouponDto>? coupons = new();

            ResponseDto? responseDto = await _couponService.GetAllCouponsAsync();

            if (responseDto is not null &&
                responseDto.Result is not null &&
                responseDto.IsSuccess)
            {
                coupons = JsonHelper.DeserializeCaseInsensitive<List<CouponDto>>(Convert.ToString(responseDto.Result)!);
            }

            return View();
        }
    }
}
