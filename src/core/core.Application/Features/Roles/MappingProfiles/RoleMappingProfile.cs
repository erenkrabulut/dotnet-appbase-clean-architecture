using AutoMapper;
using core.Application.Common.Paging;
using core.Application.Features.Roles.Dtos;
using core.Domain.Entities.Identity;

namespace core.Application.Features.Roles.MappingProfiles
{
    public sealed class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleDto>();

            CreateMap<PageResponse<Role>, PageResponse<RoleDto>>()
                .ForMember(d => d.Items, o => o.MapFrom(s => s.Items));
        }
    }
}
