using AutoMapper;
using WorkdayCalendar.API.Dtos;
using WorkdayCalendar.DomainLayer.Entities;

namespace WorkdayCalendar.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Holiday, HolidayResponseDto>();
            
            CreateMap<HolidayRequestDto, Holiday>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<HolidayUpdateRequestDto, Holiday>()
                .IncludeMembers(src => src.holiday);
        }
    }
}