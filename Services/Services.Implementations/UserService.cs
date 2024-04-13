using AutoMapper;
using Services.Repositories.Abstractions;
using Services.Abstractions;
using System.Numerics;
using Services.Contracts;
using Domain.Entities;

namespace Services.Implementations
{
    /// <summary>
    /// Сервис работы с users
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        { 
            _mapper = mapper;
            _userRepository = userRepository;   
        }

        public async Task<long> Create(UserDto userDto)
        {
            var entity = _mapper.Map<UserDto, User>(userDto);
            var res = await _userRepository.AddAsync(entity);
            await _userRepository.SaveChangesAsync();
            return res.Id;
        }

        public async Task Delete(long id)
        {
            var user = await _userRepository.GetAsync(id);
            user.Deleted = true;
            await _userRepository.SaveChangesAsync();
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            User user = await _userRepository.GetUserAsync(email);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetById(long id)
        {
            User user = await _userRepository.GetAsync(id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<ICollection<UserDto>> GetPaged(int page, int pageSize)
        {
            ICollection<User> entities = await _userRepository.GetPagedAsync(page, pageSize);

            return _mapper.Map<ICollection<UserDto>>(entities.Where(e => e.Deleted == false).ToList());
        }

        public async Task Update(long id, UserDto userDto)
        {
            var entity = _mapper.Map<UserDto, User>(userDto);
            entity.Id = id;
            _userRepository.Update(entity);
            await _userRepository.SaveChangesAsync();
        }
    }
}