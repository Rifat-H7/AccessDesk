using AccessDesk_ASP_Server.Models.DTOs.Auth;
using AccessDesk_ASP_Server.Models.Entities;
using AutoMapper;

namespace AccessDesk_ASP_Server.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, RegisterResponseDto>();
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
