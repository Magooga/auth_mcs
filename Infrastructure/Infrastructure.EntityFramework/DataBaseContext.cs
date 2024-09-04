using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Domain.Entities;
using System.Data;
using System.Reflection.Emit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; } // добавил надо ли это для третей таблицы??

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            //Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
            .Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRole>(
            j => j
                .HasOne(pt => pt.Role)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.Role_Id),  // связь с таблицей Roles через Role_Id
            j => j
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.User_Id), // связь с таблицей Users через User_Id
            j =>
            {
                j.Property(pt => pt.CreateDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                j.HasKey(t => new { t.User_Id, t.Role_Id, t.Id });
                j.ToTable("UserRoles"); // Имя таблицы
            });
   

            // название таблицы, которая создается для промежуточной сущности, из-за связи многие ко многим

        //     modelBuilder.Entity<User>().HasData(
        //         new User
        //         {
        //             Id = 1,
        //             FirstName = "root",
        //             LastName = "root",
        //             Email = "root",
        //             Hash = new Byte[20] { 21, 89, 190, 241, 252, 86, 45, 148, 227, 4, 190, 232, 124, 30, 77, 70, 43, 211, 151, 104 },       // password : "root" // role : Administrator
        //             Salt = new Byte[20] { 70, 218, 10, 125, 133, 170, 236, 193, 122, 147, 255, 100, 189, 170, 191, 243, 204, 199, 13, 118 },
        //             CreateDate = new DateTime(2021, 12, 15).ToUniversalTime(),
        //             UpDate = new DateTime(1970, 1, 1).ToUniversalTime(),
        //             Deleted = false
        //         },
        //         new User
        //         {
        //             Id = 2,
        //             FirstName = "Jane",
        //             LastName = "Kovalski",
        //             Email = "kovJ11@gmail.com",
        //             Hash = new Byte[20] { 240, 178, 9, 149, 136, 53, 111, 195, 46, 32, 194, 152, 17, 182, 139, 173, 6, 220, 130, 40 }, // password : "Jane123" // role : Teacher
        //             Salt = new Byte[20] { 234, 110, 84, 92, 115, 249, 254, 245, 205, 76, 104, 67, 126, 38, 92, 180, 35, 178, 136, 44 },
        //             CreateDate = new DateTime(2022, 01, 14).ToUniversalTime(),
        //             UpDate = new DateTime(1970, 1, 1).ToUniversalTime(),
        //             Deleted = false
        //         },
        //         new User
        //         {
        //             Id = 3,
        //             FirstName = "Sergey",
        //             LastName = "Vasiliev",
        //             Email = "sergVVV@gmail.com",
        //             Hash = new Byte[20] { 245, 127, 178, 126, 37, 21, 77, 253, 152, 28, 55, 128, 170, 79, 193, 83, 250, 92, 254, 234 }, // password : "Serg123" // role : Student
        //             Salt = new Byte[20] { 204, 182, 201, 37, 141, 244, 10, 109, 101, 37, 212, 145, 224, 11, 83, 49, 108, 50, 83, 31 },
        //             CreateDate = new DateTime(2022, 12, 11).ToUniversalTime(),
        //             UpDate = new DateTime(1970, 1, 1).ToUniversalTime(),
        //             Deleted = false
        //         });

        //     modelBuilder.Entity<Role>().HasData(
        //         new Role { Id = 1, Name = "Student", CreateDate = new DateTime(2022, 3, 1).ToUniversalTime(), UpDate = new DateTime(1970, 1, 1).ToUniversalTime(), Deleted = false},
        //         new Role { Id = 2, Name = "Teacher", CreateDate = new DateTime(2022, 2, 1).ToUniversalTime(), UpDate = new DateTime(1970, 1, 1).ToUniversalTime(), Deleted = false },
        //         new Role { Id = 3, Name = "Administrator", CreateDate = new DateTime(2022, 1, 1).ToUniversalTime(), UpDate = new DateTime(1970, 1, 1).ToUniversalTime(), Deleted = false }
        //         );

        //     modelBuilder.Entity<UserRole>().HasData(
        //         new UserRole { Id = 1, Role_Id = 3, User_Id = 1, CreateDate = new DateTime(2022, 3, 12).ToUniversalTime(), UpDate = new DateTime(1970, 1, 1).ToUniversalTime(), Deleted = false },              
        //         new UserRole { Id = 2, Role_Id = 2, User_Id = 2, CreateDate = new DateTime(2022, 6, 10).ToUniversalTime(), UpDate = new DateTime(1970, 1, 1).ToUniversalTime(), Deleted = false },
        //         new UserRole { Id = 3, Role_Id = 1, User_Id = 3, CreateDate = new DateTime(2022, 7, 17).ToUniversalTime(), UpDate = new DateTime(1970, 1, 1).ToUniversalTime(), Deleted = false }
        //         );

         }
    }
}