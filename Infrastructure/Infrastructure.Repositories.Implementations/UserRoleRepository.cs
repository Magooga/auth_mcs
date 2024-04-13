using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations
{
    public class UserRoleRepository : Repository<UserRole, long>, IUserRoleRepository
    {
        public UserRoleRepository(DbContext context) : base(context)
        {

        }
        public async Task<List<UserRole>> GetPagedAsync(int page, int itemsPerPage)
        {
            var query = GetAll();
            return await query
                .Skip((page - 1) * itemsPerPage)    // пропускает определенное количество элементов
                .Take(itemsPerPage).ToListAsync();  // извлекает определенное число элементов
        }

        public async Task<IEnumerable<UserRole>> GetAllConditionRoles(long UserId)
        {          
            var query = base.EntitySet
                .Where(b => b.User_Id == UserId)  
                .Include(b => b.Role).ToList();     // https://learn.microsoft.com/ru-ru/ef/ef6/querying/related-data

            return query;
        }

        public async Task<IEnumerable<UserRole>> GetAllConditionUsers(long roleId)
        {
            List<UserRole> query = await GetAllAsync();
            //var selectUserRoles = from u in query
            //                      where u.Role_Id == roleId
            //                      select u;
            var selectUserRoles = query.Where(u => u.Role_Id == roleId).ToList();

            return selectUserRoles;
        }

        public UserRole Get(long id1, long id2)
        {
            return base.EntitySet.Find(id1, id2);
        }

        public async Task<UserRole> GetAsync(long id1, long id2)
        {
            var entity = await base.EntitySet.FindAsync(id1, id2);


            return entity;
        }
    }
}
