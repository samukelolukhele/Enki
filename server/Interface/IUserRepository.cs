using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Model;

namespace server.Interface
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User? GetUser(string email);
        User? GetUserById(Guid id);
        bool UserExists(string email);
        bool UserExistsById(Guid id);
        ICollection<DayPlan> GetDayPlansByUser(Guid user_id);
        bool CreateUser(User user);
        bool Login(string username, string password);
        string CreateToken(string email);
        bool Save();
    }
}