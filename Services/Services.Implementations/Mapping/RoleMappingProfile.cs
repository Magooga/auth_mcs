using AutoMapper;
using Domain.Entities;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Mapping
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleDto>();

            CreateMap<RoleDto, Role>()
                .ForMember(u => u.Users, map => map.Ignore())
                .ForMember(u => u.UserRoles, map => map.Ignore());
        }
    }
}
