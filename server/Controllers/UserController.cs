using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_repo.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string email)
        {
            if (!_repo.UserExists(email))
                return NotFound();

            var user = _mapper.Map<UserDto>(_repo.GetUser(email));

            if (email == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public IActionResult GetUserById(int id)
        {
            if (!_repo.UserExistsById(id))
                return NotFound();

            var user = _mapper.Map<UserDto>(_repo.GetUserById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto newUser)
        {
            if (newUser == null)
                return BadRequest(ModelState);

            var user = _repo.GetUsers()
                .Where(u => u.email.Trim().ToUpper() == newUser.email.TrimEnd().ToUpper());

            if (user.Count() > 0)
            {
                //!Remove after testing
                Console.WriteLine(user);

                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(newUser);

            if (_repo.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving user");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}