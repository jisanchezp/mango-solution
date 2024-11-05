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

            return View(coupons);
        }

		[HttpGet]
		public async Task<IActionResult> CouponCreate()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CouponCreate(CouponDto couponDto)
		{
            if (ModelState.IsValid)
            {
                ResponseDto? responseDto = await _couponService.CreateCouponAsync(couponDto);

                if (responseDto is not null &&
                    responseDto.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
			}

            return View(couponDto);
		}

        public async Task<IActionResult> CouponDelete(int? id)
        {
            if (id is not null && id > 0)
            {
                ResponseDto? responseDto = await _couponService.GetCouponByIdAsync((int)id);

                if (responseDto is not null &&
                    responseDto.IsSuccess)
                {
                    CouponDto? couponDto = JsonHelper.DeserializeCaseInsensitive<CouponDto>(responseDto.Result.ToString());

                    return View(couponDto);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
			ResponseDto? responseDto = await _couponService.DeleteCouponAsync(couponDto.CouponId);
		
            if (responseDto is not null 
                && responseDto.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(couponDto);
        }
    }
}