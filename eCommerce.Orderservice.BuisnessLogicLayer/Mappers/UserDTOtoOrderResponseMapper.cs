using AutoMapper;
using eCommerce.Orderservice.BuisnessLogicLayer.DTO;

namespace eCommerce.Orderservice.BuisnessLogicLayer.Mappers
{
    public class UserDTOtoOrderResponseMapper:Profile
    {
        public UserDTOtoOrderResponseMapper()
        {
            CreateMap<UserDTO, OrderResponse>()
                .ForMember(dest => dest.UserPersonName, opt => opt.MapFrom(src => src.PersonName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Email));
        }
    }
}
