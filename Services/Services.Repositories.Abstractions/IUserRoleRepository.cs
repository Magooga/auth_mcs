using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с UserRoles
    /// </summary>
    public interface IUserRoleRepository : IRepository<UserRole, long>, ICompositeRepository<UserRole, long>
    {
            /// <summary>
            /// Получить постраничный список
            /// </summary>
            /// <param name="page">номер страницы</param>
            /// <param name="itemsPerPage">объем страницы</param>
            /// <returns>список ДТО users</returns>
            Task<List<UserRole>> GetPagedAsync(int page, int itemsPerPage);
            
            /// <summary>
            /// Выбирает всех юзеров с конкретной ролью
            /// </summary>
            /// <param name="roleId"></param>
            /// <returns></returns>
            Task<IEnumerable<UserRole>> GetAllConditionUsers(long roleId); 

            /// <summary>
            /// Выбирает все роли у конкретного юзера
            /// </summary>
            /// <param name="userId"></param>
            /// <returns></returns>
            Task<IEnumerable<UserRole>> GetAllConditionRoles(long userId);
    }
}
