using AutoMapper;
using Thready.Application.Dtos.Users;
using Thready.Application.Utils;
using Thready.Core.Models;

namespace Thready.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Role,
                       opt => opt.MapFrom(src => src.Role.Name));

        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Role,
                       opt => opt.Ignore());

        CreateMap<RegisterUserRequest, User>()
            .ForMember(dest => dest.Role,
                       opt => opt.Ignore())
            .ForMember(dest => dest.Password,
                       opt => opt.Ignore());
    }
}