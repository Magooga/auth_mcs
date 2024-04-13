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
    public class UserRoleMappingProfile : Profile
    {
        public UserRoleMappingProfile()
        {
            CreateMap<UserRole, UserRoleDto>()
                .ForMember(dest => dest.Role_name, map => map.MapFrom(src => src.Role.Name));

            CreateMap<UserRoleDto, UserRole>()
                .ForMember(u => u.Role, map => map.Ignore())
                .ForMember(u => u.User, map => map.Ignore());
        }
    }
}
