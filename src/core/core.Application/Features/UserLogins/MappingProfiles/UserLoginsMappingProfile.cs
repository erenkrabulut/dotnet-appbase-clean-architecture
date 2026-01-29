using AutoMapper;
using core.Application.Features.UserLogins.Dtos;
using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
