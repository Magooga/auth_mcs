using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Services.Contracts;

namespace Services.Implementations.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>()
                .ForMember(u => u.Roles, map => map.Ignore())
                .ForMember(u => u.UserRoles, map => map.Ignore());

        }
    }
}
