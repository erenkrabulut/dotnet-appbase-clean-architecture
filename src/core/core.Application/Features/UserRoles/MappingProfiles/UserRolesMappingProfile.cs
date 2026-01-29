using AutoMapper;
using core.Application.Features.Roles.Dtos;
using core.Domain.Entities.Identity;


namespace core.Application.Features.UserRoles.MappingProfiles
{
    public sealed class UserRolesMappingProfile : Profile
    {
        public UserRolesMappingProfile()
        {
            CreateMap<Role, RoleDto>();
        }
    }
}
