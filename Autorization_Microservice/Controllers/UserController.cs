using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using Autorization_Microservice.Models;
using System.Numerics;
using AutorizationMcsContract;
using Domain.Entities;
using System.Text;
using Autorization_Microservice.Secure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Autorization_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger = null, IMapper mapper = null)
        { 
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all users from db
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        // GET: api/<UserController>
        [HttpGet("list/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetList(int page, int itemsPerPage)
        {
            var result = _mapper.Map<List<UserModel>>(await _userService.GetPaged(page, itemsPerPage));

            if(result == null)
                return NotFound("No Users in DataBase");

            var contractUsers = _mapper.Map<List<UserAutorizationModel>>(result); // Map into contract between microservices

            return Ok(contractUsers);
        }

        /// <summary>
        /// Get one User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<UserController>/5
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id) 
        {
            var entity = _mapper.Map<UserModel>(await _userService.GetById(id));
            
            if (entity == null)
                return NotFound("No User with this id");

            var contractUser = _mapper.Map<UserAutorizationModel>(entity); // Map into contract between microservices

            return Ok(contractUser);
        }

        /// <summary>
        /// Get User by Email (in case, when User assign with email and password)
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Return id of User, if need user roles, go to UserRole controller</returns>
        [HttpGet("{email}/{password}")]
        public async Task<IActionResult> GetAssignUserAsync(string email, string password)
        {
            var entity = _mapper.Map<UserModel>(await _userService.GetByEmail(email));

            if (entity == null)
                return NotFound("No User with this id");

            // Verify password
            if (SecurePsw.VerifyPassword(password, entity.Hash, entity.Salt)) // if password right
            {
                return Ok(entity.Id);
            } else 
            {
                return new BadRequestResult();
            }
        }

        /// <summary>
        /// Add User in db
        /// </summary>
        /// <param name="userAutorizationModel"></param>
        /// <returns></returns>
        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> AddAsync(UserAutorizationModel userAutorizationModel)
        {
            var entity = _mapper.Map<UserModel>(userAutorizationModel);

            SecurePsw.CreateHashSalt(entity, userAutorizationModel.Password); // Create Hash and Salt for password

            return Ok(await _userService.Create(_mapper.Map<UserDto>(entity)));
        }

        /// <summary>
        /// Edit User in db by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userAutorizationModel"></param>
        /// <returns></returns>
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id, UserAutorizationModel userAutorizationModel)
        {
            var entity = _mapper.Map<UserAutorizationModel, UserModel>(userAutorizationModel);
            await _userService.Update(id, _mapper.Map<UserModel, UserDto>(entity));
            return Ok();
        }

        /// <summary>
        /// Set User "delete" property 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            await _userService.Delete(id);

            return Ok();
        }     
    }
}
