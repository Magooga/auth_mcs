using System;
using AutoMapper;
using Autorization_Microservice.Mapping;

namespace Autorization_Microservice.Settings;

public class MapperSettings
{

    public static MapperConfiguration GetMapperConfiguration()
    {
        var configMap = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserMappingProfile>();
            cfg.AddProfile<RoleMappingProfile>();
            cfg.AddProfile<UserRoleMappingProfile>();
            cfg.AddProfile<Services.Implementations.Mapping.UserMappingProfile>();
            cfg.AddProfile<Services.Implementations.Mapping.RoleMappingProfile>();
            cfg.AddProfile<Services.Implementations.Mapping.UserRoleMappingProfile>();
        });

        configMap.AssertConfigurationIsValid();
        
        return configMap;
    }
            
}
