using AutoMapper;
using core.Application.Features.UserLogins.Dtos;
using core.Domain.Entities.Identity;

namespace core.Application.Features.UserLogins.MappingProfiles
{
    public sealed class UserLoginsMappingProfile : Profile
    {
        public UserLoginsMappingProfile()
        {
            CreateMap<UserLogin, UserLoginDto>();
        }
    }
}
