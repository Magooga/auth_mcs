using System;
using Services.Contracts;
using Autorization_Microservice.Models;

namespace Auth_Tests.EntityBuilders;

public class UserBuilder
{
    public readonly UserDto _userDto = new UserDto(); 

    public void Init()
    {
        _userDto.Id = 1;
        _userDto.FirstName = "root";
        _userDto.LastName = "root";
        _userDto.Email = "root";
        _userDto.Hash = new Byte[20] { 21, 89, 190, 241, 252, 86, 45, 148, 227, 4, 190, 232, 124, 30, 77, 70, 43, 211, 151, 104 };       // password : "root" // role : Administrator
        _userDto.Salt = new Byte[20] { 70, 218, 10, 125, 133, 170, 236, 193, 122, 147, 255, 100, 189, 170, 191, 243, 204, 199, 13, 118 };
        _userDto.CreateDate = new DateTime(2021, 12, 15).ToUniversalTime();
        _userDto.UpDate = new DateTime(1970, 1, 1).ToUniversalTime();
        _userDto.Deleted = false;
    }

    public void SetTestUserDtoWithId(long id)
    {
        _userDto.Id = id;
    }

    public void SetTestUserDtoWithEmail(string email)
    {
        _userDto.Email = email;
    }
}
