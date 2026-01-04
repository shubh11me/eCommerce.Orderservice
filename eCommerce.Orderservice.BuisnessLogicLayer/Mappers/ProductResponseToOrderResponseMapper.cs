using AutoMapper;
using BuisnessLogicLayer.DTO;
using eCommerce.Orderservice.BuisnessLogicLayer.DTO;
namespace eCommerce.Orderservice.BuisnessLogicLayer.Mappers
{
    public class ProductResponseToOrderResponseMapper:Profile
    {
        public ProductResponseToOrderResponseMapper()
        {
            CreateMap<ProductResponse, OrderItemResponse>()
            .ForMember(dest => dest.ProductName, src => src.MapFrom(pr => pr.ProductName))
            .ForMember(dest => dest.ProductCategory, src => src.MapFrom(pr => pr.Category));
        }
    }
}
