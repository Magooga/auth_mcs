using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с roles
    /// </summary>
    public interface IRoleRepository : IRepository<Role, long>
    {
        Task<List<Role>> GetPagedAsync(int page, int itemsPerPage);
    }
}
