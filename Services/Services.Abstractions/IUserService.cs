using Services.Contracts;
using System.Numerics;

namespace Services.Abstractions
{
    public interface IUserService
    {
        Task<ICollection<UserDto>> GetPaged(int page, int pageSize);
        Task<UserDto> GetById(long id);
        Task<UserDto> GetByEmail(string email);
        Task<long> Create(UserDto userDto);
        Task Update(long id, UserDto userDto);
        Task Delete(long id);
    }
}