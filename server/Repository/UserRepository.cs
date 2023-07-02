using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Data;
using server.Interface;
using server.Model;

namespace server.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ServerDBContext _context;
        public UserRepository(ServerDBContext context)
        {
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

        public User? GetUserById(int id)
        {
            return _context.Users.Where(u => u.id == id).FirstOrDefault();
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(u => u.email == email);
        }

        public bool UserExistsById(int id)
        {
            return _context.Users.Any(u => u.id == id);
        }

        public ICollection<DayPlan> GetDayPlansByUser(int user_id)
        {
            return _context.DayPlans.Where(dp => dp.user_id == user_id).ToList();
        }
    }
}