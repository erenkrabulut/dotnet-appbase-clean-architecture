using AutoMapper;
using core.Application.Common.Paging;
using core.Application.Features.Permissions.Dtos;
using core.Domain.Entities.Identity;

namespace core.Application.Features.Permissions.MappingProfiles
{
    public sealed class PermissionMappingProfile : Profile
    {
        public PermissionMappingProfile()
        {
            CreateMap<Permission, PermissionDto>();

            CreateMap<PageResponse<Permission>, PageResponse<PermissionDto>>()
                .ForMember(d => d.Items, o => o.MapFrom(s => s.Items));
        }
    }
}
