using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Model;

namespace server.Dto
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string email { get; set; } = string.Empty;
        public string fName { get; set; } = string.Empty;
        public ICollection<DayPlan> day_plans { get; set; } = new List<DayPlan>();
        public DateTime created_at { get; set; }
    }
    public class CreateUserDto
    {
        public Guid Id { get; set; }
        public string email { get; set; } = null!;
        public string fName { get; set; } = null!;
        public string lName { get; set; } = null!;
        public string password { get; set; } = null!;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }

    public class LoginUserDto
    {
        public Guid id { get; }
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;

    }
}