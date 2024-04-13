using AutoMapper;
using AutorizationMcsContract;
using Autorization_Microservice.Models;
using Services.Contracts;

namespace Autorization_Microservice.Mapping
{
    public class UserRoleMappingProfile : Profile
    {
        public UserRoleMappingProfile()
        {
            CreateMap<UserRoleDto, UserRoleModel>();
            CreateMap<UserRoleModel, UserRoleDto>()
                .ForMember(u => u.Id, map => map.Ignore());

            //Map for contracts between microservices
            CreateMap<UserRoleModel, UserRoleAutorizationModel>();
            CreateMap<UserRoleAutorizationModel, UserRoleModel>()
                .ForMember(u => u.Role_name, map => map.Ignore());
        }
    }
}
