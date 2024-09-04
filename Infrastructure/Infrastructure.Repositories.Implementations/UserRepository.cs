using Services.Repositories.Abstractions;
using System.Numerics;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EntityFramework;

namespace Infrastructure.Repositories.Implementations
{
    public class UserRepository : Repository<User, long>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {       
        }

        public async Task<List<User>> GetPagedAsync(int page, int itemsPerPage)
        {
            var query = GetAll();

            return await query
                .Skip((page - 1) * itemsPerPage)    // пропускает определенное количество элементов
                .Take(itemsPerPage).ToListAsync();  // извлекает определенное число элементов
        }

        public async Task<User> GetUserAsync(string email)
        {
            List<User> query = await GetAllAsync();

            var selectUser = (from u in query
                              where u.Email == email
                              select u).FirstOrDefault();

            return selectUser;
        }
    }
}