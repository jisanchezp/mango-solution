﻿using Mango.Frontend.MVC.Models;

namespace Mango.Frontend.MVC.Services
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
