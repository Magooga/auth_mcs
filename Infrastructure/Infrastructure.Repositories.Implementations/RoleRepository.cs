using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.EntityFramework;


namespace Infrastructure.Repositories.Implementations
{
    public class RoleRepository : Repository<Role, long>, IRoleRepository
    {
        public RoleRepository(DatabaseContext context) : base(context)
        {

        }

        public async Task<List<Role>> GetPagedAsync(int page, int itemsPerPage)
        {
            var query = GetAll();
            return await query
                .Skip((page - 1) * itemsPerPage)    // пропускает определенное количество элементов
                .Take(itemsPerPage).ToListAsync();  // извлекает определенное число элементов
        }
    }
}
