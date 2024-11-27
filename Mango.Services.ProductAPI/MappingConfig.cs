using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dtos;

namespace Mango.Services.ProductAPI
{
    public static class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfiguration config = new(config =>
            {
                config.CreateMap<Product, ProductDto>();
                config.CreateMap<ProductDto, Product>();
            });

            config.AssertConfigurationIsValid();
            
            return config;
        }
    }
}
