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
        User? GetUserById(int id);
        bool UserExists(string email);
        bool UserExistsById(int id);
        ICollection<DayPlan> GetDayPlansByUser(int user_id);
    }
}