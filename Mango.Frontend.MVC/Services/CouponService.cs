using Mango.Frontend.MVC.Models;
using System;

namespace Mango.Frontend.MVC.Services
{
    public class CouponService : ICouponService
    {
        private readonly IConfiguration _config;
        private readonly IBaseService _baseService;
        private readonly string _couponApiUrl = string.Empty;
        private readonly string CONTROLLER_ROUTE = "api/Coupons";

        public CouponService(IConfiguration config, IBaseService baseService)
        {
            _config = config;
            _baseService = baseService;
            string? couponApiUrl = _config.GetValue<string>("Services:CouponAPI");

            if (string.IsNullOrEmpty(couponApiUrl))
            {
                _couponApiUrl = $"{_couponApiUrl}/{CONTROLLER_ROUTE}";
            }
        }

        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {

            RequestDto requestDto = new()
            {
                Url = $"{_couponApiUrl}/GetByCode/{couponCode}",
            };

            ResponseDto? response = await _baseService.SendAsync(requestDto);
            return response;
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            RequestDto requestDto = new()
            {
                Url = _couponApiUrl,
            };

            ResponseDto? response = await _baseService.SendAsync(requestDto);
            return response;
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            RequestDto requestDto = new()
            {
                Url = $"{_couponApiUrl}/{id}",
            };

            ResponseDto? response = await _baseService.SendAsync(requestDto);
            return response;
        }

        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            RequestDto requestDto = new()
            {
                HttpVerb = Enums.ApiTypeEnum.POST,
                Url = _couponApiUrl,
                Data = couponDto
            };

            ResponseDto? response = await _baseService.SendAsync(requestDto);
            return response;
        }    

        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
        {
            RequestDto requestDto = new()
            {
                HttpVerb = Enums.ApiTypeEnum.PUT,
                Url = _couponApiUrl,
                Data = couponDto
            };

            ResponseDto? response = await _baseService.SendAsync(requestDto);
            return response;
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            RequestDto requestDto = new()
            {
                HttpVerb = Enums.ApiTypeEnum.DELETE,
                Url = $"{_couponApiUrl}/{id}",
            };

            ResponseDto? response = await _baseService.SendAsync(requestDto);
            return response;
        }
    }
}
