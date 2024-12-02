using Mango.Frontend.MVC.Helper;
using Mango.Frontend.MVC.Models.Dtos;
using Mango.Frontend.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Mango.Frontend.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto>? products = new();

            ResponseDto? responseDto = await _productService.GetAllProductsAsync();

            if (responseDto is not null &&
                responseDto.Result is not null &&
                responseDto.IsSuccess)
            {
                products = JsonHelper.DeserializeCaseInsensitive<List<ProductDto>?>(Convert.ToString(responseDto.Result)!);
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }

            return View(products);
        }

        [HttpGet]
        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {
            if (ModelState.IsValid) 
            {
                ResponseDto? responseDto = await _productService.CreateProductAsync(productDto);

                if (responseDto is not null &&
                    responseDto.IsSuccess)
                {
                    TempData["success"] = "An amazing product has been born! :O";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                }
            }

            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit(int? id)
        {
            if (id is not null && id > 0)
            {
                ResponseDto? responseDto = await _productService.GetProductByIdAsync((int)id);

                if (responseDto is not null
                    && responseDto.Result is not null
                    && responseDto.IsSuccess)
                {
                    ProductDto? productDto = JsonHelper.DeserializeCaseInsensitive<ProductDto>(Convert.ToString(responseDto.Result)!);
                    return View(productDto);
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                }
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult ProductEdit(ProductDto productDto)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProductDelete(int? id)
        {
            if (id is not null && id > 0)
            {
                ResponseDto? responseDto = await _productService.GetProductByIdAsync((int)id);

                if (responseDto is not null &&
                    responseDto.IsSuccess)
                {
                    ProductDto? productDto = JsonHelper.DeserializeCaseInsensitive<ProductDto>(Convert.ToString(responseDto.Result)!);

                    return View(productDto);
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            ResponseDto? responseDto = await _productService.DeleteProductAsync(productDto.ProductId);

            if (responseDto is not null &&
                responseDto.IsSuccess)
            {
                TempData["success"] = "Product is no more...";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }

            return View(productDto);
        }
    }
}
