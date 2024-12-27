using AutoMapper;
using Order.API.Dtos;

namespace Order.API.Utitles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Entities.Order, OrderForCreateDto>().ReverseMap();
            CreateMap<Entities.Order, OrderForUpdateDto>().ReverseMap();
            CreateMap<Entities.Order, OrderForReturnDto>().ReverseMap();
        }
    }
}
