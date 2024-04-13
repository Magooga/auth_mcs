using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using Autorization_Microservice.Models;
using System.Numerics;
using AutorizationMcsContract;
using System.Security.Cryptography;
using System.Security.Policy;
using Domain.Entities;
using System.Text;

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
        public async Task<IActionResult> Get(long id) 
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
        public async Task<IActionResult> GetAssignUser(string email, string password)
        {
            var entity = _mapper.Map<UserModel>(await _userService.GetByEmail(email));

            if (entity == null)
                return NotFound("No User with this id");

            // Verify password
            if (VerifyPassword(password, entity.Hash, entity.Salt)) // if password right
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
        public async Task<IActionResult> Add(UserAutorizationModel userAutorizationModel)
        {
            var entity = _mapper.Map<UserModel>(userAutorizationModel);

            CreateHashSalt(entity, userAutorizationModel.Password); // Create Hash and Salt for password

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
        public async Task<IActionResult> Edit(int id, UserAutorizationModel userAutorizationModel)
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
        public async Task<IActionResult> Delete(long id)
        {
            await _userService.Delete(id);

            return Ok();
        }

        void CreateHashSalt(UserModel user, string password) 
        {
            byte[] mySalt;
            user.Hash = HashPassword(password, out mySalt);
            user.Salt = mySalt;
        }

        byte[] HashPassword(string password, out byte[] salt)
        {
            const int keySize = 20;
            const int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize
                );

            return hash;
        }

        bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            const int keySize = 20;
            const int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return hashToCompare.SequenceEqual(hash);
        }
    }
}
