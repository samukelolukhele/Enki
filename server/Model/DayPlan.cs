using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Model
{
    public class DayPlan
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public User user { get; } = null!;
        public ICollection<Task> tasks { get; } = new List<Model.Task>();
    }
}