using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Services.Contracts;

namespace Services.Abstractions
{
    public interface IRoleService
    {
        /// <summary>
        /// Получить список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <returns></returns>
        Task<ICollection<RoleDto>> GetPaged(int page, int pageSize);
        
        /// <summary>
        /// Получить  
        /// </summary>
        /// <param name="id"> идетификатор </param>
        /// <returns>ДТО Роли</returns>
        Task<RoleDto> GetById(long id);

        /// <summary>
        /// Создать
        /// </summary>
        /// <param name="roleDto">ДТО Роли</param>
        /// <returns></returns>
        Task<long> Create(RoleDto roleDto);

        /// <summary>
        /// Изменить
        /// </summary>
        /// <param name="id">идетификатор</param>
        /// <param name="roleDto">ДТО Роли</param>
        /// <returns></returns>
        Task Update(long id, RoleDto roleDto);

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id">идетификатор</param>
        /// <returns></returns>
        Task Delete(long id); 
    }
}
