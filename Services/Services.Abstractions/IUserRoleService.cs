using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IUserRoleService
    {
        Task<ICollection<UserRoleDto>> GetPaged(int page, int pageSize);
        Task<UserRoleDto> GetById(long id);
        Task<long> Create(UserRoleDto userRoleDto);
        Task Update(long id, UserRoleDto userRoleDto);
        Task Delete(long userId, long roleId);

        Task<List<UserRoleDto>> GetByConditionRoles(long userId);
        Task<List<UserRoleDto>> GetByConditionUsers(long roleId);
    }
}
