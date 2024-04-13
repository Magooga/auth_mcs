using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с users
    /// </summary>
    public interface IUserRepository : IRepository<User, long>    
    {
        /// <summary>
        /// Получить постраничный список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="itemsPerPage">объем страницы</param>
        /// <returns>список ДТО users</returns>
        Task<List<User>> GetPagedAsync(int page, int itemsPerPage);

        /// <summary>
        /// Get User from db by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User> GetUserAsync(string email);
    }
}
