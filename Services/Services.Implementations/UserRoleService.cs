using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
using Services.Contracts;
using Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IMapper _mapper;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IMapper mapper, IUserRoleRepository userRoleRepository)
        { 
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;   
        }

    public async Task<long> Create(UserRoleDto userRoleDto)
        {
            var entity = _mapper.Map<UserRoleDto, UserRole>(userRoleDto);
            var res = await _userRoleRepository.AddAsync(entity);
            await _userRoleRepository.SaveChangesAsync();
            return res.Id;
        }

        public async Task Delete(long userId, long roleId)
        {
            var entity = await _userRoleRepository.GetAsync(userId, roleId);
            entity.Deleted = true;
            await _userRoleRepository.SaveChangesAsync();
        }

        public async Task<List<UserRoleDto>> GetByConditionRoles(long userId)
        {
            var userRoleLst = await _userRoleRepository.GetAllConditionRoles(userId);
            
            var result = _mapper.Map<List<UserRoleDto>>(userRoleLst);

            return result;
        }

        public async Task<List<UserRoleDto>> GetByConditionUsers(long roleId)
        {
            var userRoleLst = await _userRoleRepository.GetAllConditionUsers(roleId);

            var result = _mapper.Map<List<UserRoleDto>>(userRoleLst);

            return result;
        }

        public async Task<UserRoleDto> GetById(long id)
        {
            UserRole userRole = await _userRoleRepository.GetAsync(id);

            return _mapper.Map<UserRoleDto>(userRole);
        }

        public async Task<ICollection<UserRoleDto>> GetPaged(int page, int pageSize)
        {
            ICollection<UserRole> entities = await _userRoleRepository.GetPagedAsync(page, pageSize);

            return _mapper.Map<ICollection<UserRoleDto>>(entities
                                                         .Where(e => e.Deleted == false) // Filter deleted entities
                                                         .ToList()); 
        }

        public async Task Update(long id, UserRoleDto userRoleDto)
        {
            throw new NotImplementedException();
        }
    }
}
