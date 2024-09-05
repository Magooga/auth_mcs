namespace AuthorizationTests;

using Moq;
using Services.Abstractions;
using Services.Contracts;
using Autorization_Microservice.Controllers;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Autorization_Microservice.Mapping;
using System.Web.Http.Results;
using AutorizationMcsContract;

public class UnitTest1
{
    [Fact]
    public async void GetReturnsUserWithSameId()
    {
        // Arrange
        var configMap = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserMappingProfile>();
        });
        var mapper = configMap.CreateMapper();
        

        var mockService = new Mock<IUserService>();
        mockService.Setup(service => service.GetById(1).Result).Returns(GetTestUser(1));
        
        var controller = new UserController(userService: mockService.Object, mapper: mapper);
        
        // Act
        var actionResult = await controller.Get(1);
        var contentResult = actionResult as ObjectResult;

        // Assert
        Assert.NotNull(contentResult);
        Assert.True(contentResult is OkObjectResult);
        Assert.Equal(200, contentResult.StatusCode);
        Assert.IsType<UserAutorizationModel>(contentResult.Value);
    }

    [Fact]
    public async void GetById_User_ReturnUser()
    {
        // Arrange

        // Act

        // Assert
    }

    private UserDto GetTestUser(int id)
    {
        return new UserDto 
        {
            Id = id, 
            FirstName = "Igor", 
            LastName = "Gorev", 
            Email = "email", 
            Hash = new Byte[20], 
            Salt = new Byte[20],
            CreateDate = DateTime.Now,
            UpDate = DateTime.Now,
            Deleted = false 
        };
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal(0,0);
    }
}