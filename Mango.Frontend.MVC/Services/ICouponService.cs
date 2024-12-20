﻿using Mango.Frontend.MVC.Models;

namespace Mango.Frontend.MVC.Services
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponAsync(string couponCode);
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> GetCouponByIdAsync(int id);
        Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> UpdateCouponAsync(CouponDto couponCode);
        Task<ResponseDto?> DeleteCouponAsync(int id);
    }
}