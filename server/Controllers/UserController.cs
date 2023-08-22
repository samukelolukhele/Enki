using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Dto;
using server.Interface;
using server.Model;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public UserController(IUserRepository _repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = _repo;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetUserDto>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<GetUserDto>>(_repo.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [Authorize]
        [HttpGet("email/{email}")]
        [ProducesResponseType(200, Type = typeof(GetUserDto))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string email)
        {
            if (!_repo.UserExists(email))
                return NotFound();

            var user = _mapper.Map<GetUserDto>(_repo.GetUser(email));

            if (email == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(GetUserDto))]
        [ProducesResponseType(400)]
        public IActionResult GetUserById(Guid id)
        {
            if (!_repo.UserExistsById(id))
                return NotFound();

            var user = _mapper.Map<GetUserDto>(_repo.GetUserById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateUser([FromBody] CreateUserDto newUser)
        {
            if (newUser == null)
                return BadRequest(ModelState);


            if (_repo.UserExists(newUser.email))
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(newUser);

            if (!_repo.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving user");
                return StatusCode(500, ModelState);
            }

            var token = _repo.CreateToken(userMap.email);

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public IActionResult Login(string username, string password)
        {
            if (!_repo.UserExists(username))
                return Unauthorized("The email or password is incorrect.");

            if (_repo.Login(username, password) == false)
                return Unauthorized("The email or password is incorrect.");

            var token = _repo.CreateToken(username);
            return Ok(token);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(Guid id, [FromBody] User updatedUser)

        {
            if (updatedUser == null)
                return BadRequest("No new user data added");

            if (!_repo.UserExistsById(id))
                return NotFound("User does not exist");

            if (!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<User>(updatedUser);

            if (!_repo.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the user.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(Guid id)
        {
            if (!_repo.UserExistsById(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userToDelete = _repo.GetUserById(id);

            if (!_repo.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the user.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }

}