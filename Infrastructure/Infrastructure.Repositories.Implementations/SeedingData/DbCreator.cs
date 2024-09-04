using System;
using Services.Repositories.Abstractions;
using Infrastructure.EntityFramework;
using Domain.Entities;

namespace Infrastructure.Repositories.Implementations.SeedingData;

public class DbCreator : IDbCreator
{
    private readonly DatabaseContext _dataContext;

    public DbCreator(DatabaseContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void Create()
    {
        var userFirst = _dataContext.Set<User>().FirstOrDefault();

        if(userFirst == null)
        {
            _dataContext.AddRange(FakeDataFactory.Users);
            _dataContext.SaveChanges();

            _dataContext.AddRange(FakeDataFactory.Roles);
            _dataContext.SaveChanges();

            _dataContext.AddRange(FakeDataFactory.UserRoles);
            _dataContext.SaveChanges();
        }
    }
}
