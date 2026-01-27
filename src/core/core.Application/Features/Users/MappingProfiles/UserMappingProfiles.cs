using AutoMapper;
using core.Application.Common.Paging;
using core.Application.Features.Users.Dtos;
using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Users.MappingProfiles
{
    public sealed class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<PageResponse<User>, PageResponse<UserDto>>()
                .ForMember(d => d.Items, opt => opt.MapFrom(s => s.Items));
        }
    }
}
