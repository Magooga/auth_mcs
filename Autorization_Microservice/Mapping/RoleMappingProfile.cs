using AutoMapper;
using Services.Contracts;
using Autorization_Microservice.Models;
using AutorizationMcsContract;

namespace Autorization_Microservice.Mapping
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<RoleDto, RoleModel>();
            CreateMap<RoleModel, RoleDto>();

            //Map for contracts between microservices
            CreateMap<RoleModel, RoleAutorizationModel>();
            CreateMap<RoleAutorizationModel, RoleModel>();
                //.ForMember( r => r.Id, map => map.Ignore());
        }

    }
}
