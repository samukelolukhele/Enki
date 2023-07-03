using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Model
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
        public string fName { get; set; } = null!;
        public string lName { get; set; } = null!;
        public ICollection<DayPlan> day_plans { get; } = new List<DayPlan>();
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}