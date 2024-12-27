using AutoMapper;
using Notification.API.Dtos;

namespace Notification.API.Utitles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Entities.Notification, NotificationForCreateDto>().ReverseMap();
            CreateMap<Entities.Notification, NotificationForReturnDto>().ReverseMap();
        }
    }
}
