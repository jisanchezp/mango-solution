using Mango.Frontend.MVC.Helper;
using Mango.Frontend.MVC.Models.Dtos;
using Mango.Frontend.MVC.Services.Interfaces;
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
            else
            {
                TempData["error"] = responseDto?.Message;
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
                    TempData["success"] = "A beautiful Coupon has been born ^^!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
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
                else
                {
                    TempData["error"] = responseDto?.Message;
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
                TempData["success"] = "Coupon has left us...";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }

            return View(couponDto);
        }
    }
}