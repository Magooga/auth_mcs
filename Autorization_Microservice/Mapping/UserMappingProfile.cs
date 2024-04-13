using AutoMapper;
using Services.Contracts;
using Autorization_Microservice.Models;
using AutorizationMcsContract;

namespace Autorization_Microservice.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDto, UserModel>();
            CreateMap<UserModel, UserDto>();

            //Map for contracts between microservices
            CreateMap<UserModel, UserAutorizationModel>().ForMember(u => u.Password, map => map.Ignore());
            CreateMap<UserAutorizationModel, UserModel>();
        }
    }
}
