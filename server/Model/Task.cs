using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Model
{
    public class Task
    {
        public int id { get; set; }
        public int day_plan_id { get; set; }
        public string title { get; set; } = null!;
        public string? description { get; set; } = default;
        public bool is_completed { get; set; }
        public DayPlan day_plan { get; } = null!;
        public ICollection<Milestone> milestones { get; } = new List<Milestone>();
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deadline { get; set; }
    }
}