namespace AuthorizationTests;

using Moq;
using Services.Abstractions;
using Services.Contracts;
using Autorization_Microservice.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Autorization_Microservice.Mapping;
using System.Web.Http.Results;
using AutorizationMcsContract;
using AutoFixture;
using AutoFixture.AutoMoq;
using Autorization_Microservice.Settings;
using FluentAssertions;
using Auth_Tests.EntityBuilders;
using System.Net;

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
        var userBuilder = new UserBuilder();
        userBuilder.Init().SetTestUserDtoId(userId);
 
        _userServiceMock.Setup(service => service.GetById(userId).Result).Returns(userBuilder.GetEntity());
        
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
    public async void GetById_UserIsNotFound_ReturnsNotFound()
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
        //contentResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
        Assert.IsType<NotFoundObjectResult>(actionResult);
        contentResult.Value.Should().Be(strReturnErr);       
    }


    [Fact]
    public async void GetAssignUser_User_ReturnsId()
    {
        // Arrange
        var email = "root";
        var psw = "root";
        long userId = 23;

        var userBuilder = new UserBuilder();
        userBuilder.Init().SetTestUserDtoId(userId);

        _userServiceMock.Setup(service => service.GetByEmail(email).Result).Returns(userBuilder.GetEntity());

        // Act
        var actionResult = await _userController.GetAssignUserAsync(email, psw);
        var contentResult = actionResult as ObjectResult;

        // Assert
        Assert.NotNull(contentResult);
        Assert.True(contentResult is OkObjectResult);
        Assert.Equal(200, contentResult.StatusCode);
        Assert.Equal(userId, contentResult.Value);
    }

    [Fact]
    public async void GetAssignUser_User_ReturnsBadRequestResult()
    {
        // Arrange
        var email = "root";
        var psw = "rootWrong";
        long userId = 23;
        var strReturnErr = "Wrong Password!";

        var userBuilder = new UserBuilder();
        userBuilder.Init().SetTestUserDtoId(userId);

        _userServiceMock.Setup(service => service.GetByEmail(email).Result).Returns(userBuilder.GetEntity());

        // Act
        var actionResult = await _userController.GetAssignUserAsync(email, psw);
        var contentResult = actionResult as ObjectResult;

        // Assert
        Assert.IsType<BadRequestObjectResult>(actionResult);
        contentResult.Value.Should().Be(strReturnErr);
    }
}
