using AutoMapper;
using Autorization_Microservice.Models;
using AutorizationMcsContract;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using Services.Implementations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Autorization_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;
        private IMapper _mapper;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService roleService, ILogger<RoleController> logger, IMapper mapper)
        {
            _roleService = roleService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<RoleController>
        [HttpGet]
        public async Task<IActionResult> GetList(int page, int itemsperpage)
        {
            var result = _mapper.Map<List<RoleModel>>(await _roleService.GetPaged(page, itemsperpage));

            if (result == null)
                return NotFound("No Roles in DataBase");

            var contractRoles = _mapper.Map<List<RoleAutorizationModel>>(result); // Map into contract between microservices

            return Ok(contractRoles);
        }

        // GET api/<RoleController>/5
        [HttpGet("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = _mapper.Map<RoleModel>(await _roleService.GetById(id));

            if (result == null)
                return NotFound("No Roles in DataBase with this id");

            var contractRole = _mapper.Map<RoleAutorizationModel>(result); // Map into contract between microservices

            return Ok(contractRole);
        }

        /// <summary>
        /// Add Role in db
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        // POST api/<RoleController>
        [HttpPost]
        public async Task<IActionResult> Post(RoleAutorizationModel role)
        {
            var entity = _mapper.Map<RoleModel>(role);

            return Ok(await _roleService.Create(_mapper.Map<RoleDto>(entity)));
        }

        /// <summary>
        /// Edit Role in db by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] RoleAutorizationModel role)
        {
            var entity = _mapper.Map<RoleModel>(role);
            await _roleService.Update(id, _mapper.Map<RoleDto>(entity));
            return Ok();
        }

        /// <summary>
        /// Set Role "delete" property
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _roleService.Delete(id);
            return Ok();
        }
    }
}
