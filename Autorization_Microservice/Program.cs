
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;
using Services.Abstractions;
using Services.Implementations;
using AutoMapper;
using Autorization_Microservice.Mapping;

namespace Autorization_Microservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DatabaseContext>( x =>
            {
                x.UseNpgsql(builder.Configuration.GetConnectionString("db"));

                Console.WriteLine("builder.Configuration.GetConnectionString(\"db\") = " + builder.Configuration.GetConnectionString("db"));
            });

            builder.Services.AddScoped(typeof(DbContext), typeof(DatabaseContext));

            builder.Services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            builder.Services.AddTransient(typeof(IRoleRepository), typeof(RoleRepository));
            builder.Services.AddTransient(typeof(IUserRoleRepository), typeof(UserRoleRepository));

            builder.Services.AddTransient(typeof(IUserService), typeof(UserService));
            builder.Services.AddTransient(typeof(IRoleService), typeof(RoleService));
            builder.Services.AddTransient(typeof(IUserRoleService), typeof(UserRoleService));

            var configMap = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMappingProfile>();
                cfg.AddProfile<RoleMappingProfile>();
                cfg.AddProfile<UserRoleMappingProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.UserMappingProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.RoleMappingProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.UserRoleMappingProfile>();
            });

            var mapper = configMap.CreateMapper();

            builder.Services.AddSingleton<IMapper>(mapper);

            var app = builder.Build();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // иначе ошибка : error :  'timestamp with time zone' literal cannot be generated for Unspecified DateTime: a UTC DateTime is required


            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}