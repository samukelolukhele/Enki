using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using server.Data;
using server.Interface;
using server.Model;
using Microsoft.IdentityModel.Tokens;
using server.Dto;

namespace server.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ServerDBContext _context;
        private readonly IConfiguration _config;
        public UserRepository(ServerDBContext context, IConfiguration _config)
        {
            this._config = _config;
            _context = context;
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.id).ToList();
        }

        public User? GetUser(string email)
        {
            return _context.Users.Where(u => u.email == email).FirstOrDefault();
        }

        public User? GetUserById(Guid id)
        {
            return _context.Users.Where(u => u.id == id).FirstOrDefault();
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(u => u.email.Trim().ToLower() == email.TrimEnd().ToLower());
        }

        public bool UserExistsById(Guid id)
        {
            return _context.Users.Any(u => u.id == id);
        }

        public ICollection<DayPlan> GetDayPlansByUser(Guid user_id)
        {
            return _context.DayPlans.Where(dp => dp.user_id == user_id).ToList();
        }

        public bool CreateUser(User user)
        {
            user.email = user.email.Trim().ToLower();
            user.id = Guid.NewGuid();
            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
            _context.Users.Add(user);
            return Save();
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Entry(user);

            user.updated_at = DateTime.UtcNow;

            _context.Entry(user).Property(x => x.fName).IsModified = user.fName.Trim() != null;
            _context.Entry(user).Property(x => x.lName).IsModified = user.lName.Trim() != null;
            _context.Entry(user).Property(x => x.updated_at).IsModified = true;

            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return Save();
        }

        public bool Login(string username, string password)
        {
            var user = GetUser(username);
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.password);

            return isValidPassword;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public string CreateToken(Guid id)
        {
            List<Claim> claims = new List<Claim>
            {
             new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:Token"]));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(45),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


    }
}