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
using AutoFixture;
using AutoFixture.AutoMoq;
using Autorization_Microservice.Settings;
using FluentAssertions;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly IMapper mapper;
    private readonly UserController _userController;

    public UserControllerTests()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());

        _userServiceMock = fixture.Freeze<Mock<IUserService>>();

        var configMap = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserMappingProfile>();
        });
        mapper = configMap.CreateMapper();

        _userController = new UserController(userService: _userServiceMock.Object, mapper: mapper);
    }

    [Fact]
    public async void GetByIdAsync_User_ReturnsUserAutorizationModel()
    {
        // Arrange
        var userId = 1;
        _userServiceMock.Setup(service => service.GetById(userId).Result).Returns(GetTestUser(userId));
        
        // Act
        var actionResult = await _userController.GetByIdAsync(userId);
        var contentResult = actionResult as ObjectResult;

        // Assert
        Assert.NotNull(contentResult);
        Assert.True(contentResult is OkObjectResult);
        Assert.Equal(200, contentResult.StatusCode);
        Assert.IsType<UserAutorizationModel>(contentResult.Value);
    }

    [Fact]
    public async void GetById_UserIsNotFound_ReturnsNoUserWithThisId()
    {
        // Arrange
        var userId = 99;
        var strReturnErr = "No User with this id";
        UserDto? user = null; 

        _userServiceMock.Setup(service => service.GetById(userId).Result).Returns(user);

        // Act
        var actionResult = await _userController.GetByIdAsync(userId);
        var contentResult = actionResult as ObjectResult;

        // Assert
        contentResult.Value.Should().Be(strReturnErr);
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