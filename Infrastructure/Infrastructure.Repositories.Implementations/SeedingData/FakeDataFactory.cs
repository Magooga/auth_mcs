using System;
using Domain.Entities;

namespace Infrastructure.Repositories.Implementations.SeedingData;

public static class FakeDataFactory
{
    public static IEnumerable<User> Users => new List<User>()
    {
        new User
        {
            Id = 1,
            FirstName = "root",
            LastName = "root",
            Email = "root",
            Hash = new Byte[20] { 21, 89, 190, 241, 252, 86, 45, 148, 227, 4, 190, 232, 124, 30, 77, 70, 43, 211, 151, 104 },       // password : "root" // role : Administrator
            Salt = new Byte[20] { 70, 218, 10, 125, 133, 170, 236, 193, 122, 147, 255, 100, 189, 170, 191, 243, 204, 199, 13, 118 },
            CreateDate = new DateTime(2021, 12, 15).ToUniversalTime(),
            UpDate = new DateTime(1970, 1, 1).ToUniversalTime(),
            Deleted = false
        },
        new User
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Kovalski",
            Email = "kovJ11@gmail.com",
            Hash = new Byte[20] { 240, 178, 9, 149, 136, 53, 111, 195, 46, 32, 194, 152, 17, 182, 139, 173, 6, 220, 130, 40 }, // password : "Jane123" // role : Teacher
            Salt = new Byte[20] { 234, 110, 84, 92, 115, 249, 254, 245, 205, 76, 104, 67, 126, 38, 92, 180, 35, 178, 136, 44 },
            CreateDate = new DateTime(2022, 01, 14).ToUniversalTime(),
            UpDate = new DateTime(1970, 1, 1).ToUniversalTime(),
            Deleted = false
        },
        new User
        {
            Id = 3,
            FirstName = "Sergey",
            LastName = "Vasiliev",
            Email = "sergVVV@gmail.com",
            Hash = new Byte[20] { 245, 127, 178, 126, 37, 21, 77, 253, 152, 28, 55, 128, 170, 79, 193, 83, 250, 92, 254, 234 }, // password : "Serg123" // role : Student
            Salt = new Byte[20] { 204, 182, 201, 37, 141, 244, 10, 109, 101, 37, 212, 145, 224, 11, 83, 49, 108, 50, 83, 31 },
            CreateDate = new DateTime(2022, 12, 11).ToUniversalTime(),
            UpDate = new DateTime(1970, 1, 1).ToUniversalTime(),
            Deleted = false
        }
    };

    public static IEnumerable<Role> Roles => new List<Role>()
    {
        new Role 
        { 
            Id = 1, 
            Name = "Student", 
            CreateDate = new DateTime(2022, 3, 1).ToUniversalTime(), 
            UpDate = new DateTime(1970, 1, 1).ToUniversalTime(), 
            Deleted = false
            },
            new Role 
            { 
                Id = 2, 
                Name = "Teacher", 
                CreateDate = new DateTime(2022, 2, 1).ToUniversalTime(), 
                UpDate = new DateTime(1970, 1, 1).ToUniversalTime(), 
                Deleted = false 
            },
            new Role 
            { 
                Id = 3, 
                Name = "Administrator", 
                CreateDate = new DateTime(2022, 1, 1).ToUniversalTime(), 
                UpDate = new DateTime(1970, 1, 1).ToUniversalTime(), 
                Deleted = false 
            }
    };

    public static IEnumerable<UserRole> UserRoles => new List<UserRole>()
    {
        new UserRole 
        { 
            Id = 1, 
            Role_Id = 3, 
            User_Id = 1, 
            CreateDate = new DateTime(2022, 3, 12).ToUniversalTime(), 
            UpDate = new DateTime(1970, 1, 1).ToUniversalTime(), 
            Deleted = false 
        },              
        new UserRole 
        { 
            Id = 2, 
            Role_Id = 2, 
            User_Id = 2, 
            CreateDate = new DateTime(2022, 6, 10).ToUniversalTime(), 
            UpDate = new DateTime(1970, 1, 1).ToUniversalTime(), 
            Deleted = false 
        },
        new UserRole 
        { 
            Id = 3, 
            Role_Id = 1, 
            User_Id = 3, 
            CreateDate = new DateTime(2022, 7, 17).ToUniversalTime(), 
            UpDate = new DateTime(1970, 1, 1).ToUniversalTime(), 
            Deleted = false 
        }
    }; 
}
