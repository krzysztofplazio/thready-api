using AutoMapper;
using Thready.Application.Dtos.Users;
using Thready.Core.Models;

namespace Thready.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(x => x.Role,
                       opt => opt.MapFrom(x => x.Role.Name));
        CreateMap<UserDto, User>()
            .ForMember(x => x.Role,
                       opt => opt.Ignore());

        CreateMap<RegisterUserRequest, UserDto>()
            .ReverseMap()
            .ForMember(x => x.Role,
                       opt => opt.Ignore())
            .ForMember(x => x.Password, 
                       opt => opt.Ignore());
    }
}