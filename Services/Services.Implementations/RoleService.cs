using AutoMapper;
using Services.Abstractions;
using Services.Contracts;
using Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IMapper mapper, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<long> Create(RoleDto roleDto)
        {
            var entity = _mapper.Map<RoleDto, Role>(roleDto);
            var res = await _roleRepository.AddAsync(entity);
            await _roleRepository.SaveChangesAsync();
            return res.Id;
        }

        public async Task Delete(long id)
        {
            var role = await _roleRepository.GetAsync(id);
            role.Deleted = true;
            await _roleRepository.SaveChangesAsync();   
        }

        public async Task<RoleDto> GetById(long id)
        {
            Role role = await _roleRepository.GetAsync(id);
            return _mapper.Map<RoleDto>(role);  
        }

        public async Task<ICollection<RoleDto>> GetPaged(int page, int pageSize)
        {
            ICollection<Role> entities = await _roleRepository.GetPagedAsync(page, pageSize);
            
            return _mapper.Map<ICollection<RoleDto>>(entities.Where(e => e.Deleted == false).ToList());    
        }

        public async Task Update(long id, RoleDto roleDto)
        {
            var entity = _mapper.Map<Role>(roleDto);
            entity.Id = id;
            _roleRepository.Update(entity);
            await _roleRepository.SaveChangesAsync();
        }
    }
}
