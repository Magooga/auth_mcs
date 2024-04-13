using AutoMapper;
using Autorization_Microservice.Models;
using AutorizationMcsContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Abstractions;
using Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Autorization_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private IUserRoleService _userRoleService;
        private IMapper _mapper;
        private readonly ILogger<UserRoleController> _logger;

        public UserRoleController(IUserRoleService userRoleService, IMapper mapper, ILogger<UserRoleController> logger)
        { 
            _mapper = mapper;
            _userRoleService = userRoleService; 
            _logger = logger;
        }

        // GET: api/<UserRoleController>
        [HttpGet]
        public async Task<IActionResult> GetList(int page, int itemsPerPage)
        {
            ICollection<UserRoleModel> entities = _mapper.Map<List<UserRoleModel>>(await _userRoleService.GetPaged(page, itemsPerPage));

            if (entities == null)
                return NotFound("No Users Roles in DataBase");

            var resultContract = _mapper.Map<List<UserRoleAutorizationModel>>(entities);

            return Ok(resultContract);
        }

        /// <summary>
        /// One endpoint for two parameters: UserId, RoleId 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        // GET api/<UserRoleController>/{UserId}
        //[HttpGet("{UserId:long}/{RoleId:long}")]
        //public async Task<IActionResult> GetRoles(long UserId = 0, long RoleId = 0)
        //{
        //    dynamic result;

        //    if (RoleId == 0)
        //        //ICollection<UserRoleModel> result = _mapper.Map<List<UserRoleModel>>(await _userRoleService.GetByConditionRoles(UserId));
        //        result = _mapper.Map<List<UserRoleModel>>(await _userRoleService.GetByConditionRoles(UserId));
        //    else if (UserId == 0)
        //        //ICollection<UserRoleModel> result = _mapper.Map<List<UserRoleModel>>(await _userRoleService.GetByConditionUsers(RoleId));
        //        result = _mapper.Map<List<UserRoleModel>>(await _userRoleService.GetByConditionUsers(RoleId));
        //    else
        //        result = 0;

        //    return Ok(result);
        //}

        // Get all (string) roles by User id
        // GET api/<UserRoleController>/{UserId}
        [HttpGet("{UserId:long}")]
        public async Task<IActionResult> GetRoles(long UserId)
        {
            List<string> resultOut;

            var result = _mapper.Map<List<UserRoleModel>>(await _userRoleService.GetByConditionRoles(UserId));

            if (result.Count != 0)
            {
                resultOut = new List<string>();

                // Extract Role name from objects
                foreach (var v in result)
                {
                    resultOut.Add(v.Role_name);
                }

                return Ok(resultOut);
            }
            else // no user with this id
            {
                return new NotFoundResult();
            }
        }

        // POST api/<UserRoleController>
        [HttpPost]
        public async Task<IActionResult> Add(UserRoleAutorizationModel userRole)
        {
            var result = _mapper.Map<UserRoleAutorizationModel, UserRoleModel>(userRole);
            return Ok(await _userRoleService.Create(_mapper.Map<UserRoleModel, UserRoleDto>(result)));
        }

        // This endpoint (Edit/Update) userRole don't need because we do this by Add and Delete methods !!
        //// PUT api/<UserRoleController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<UserRoleController>/5
        [HttpDelete("{userId:long}/{roleId:long}")]
        public async Task<IActionResult> Delete(long userId, long roleId)
        {
            await _userRoleService.Delete(userId, roleId);

            return Ok();
        }
    }
}
