using AutoMapper;
using Thready.API.Dtos.Users;
using Thready.Models.Models;

namespace Thready.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<RegisterUserRequest, UserDto>()
            .ReverseMap()
            .ForMember(x => x.Role,
                       opt => opt.Ignore())
            .ForMember(x => x.Password, 
                       opt => opt.Ignore());
    }
}