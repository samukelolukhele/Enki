using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Model
{
    public class Milestone
    {
        public int id { get; set; }
        public int task_id { get; set; }
        public string title { get; set; } = null!;
        public string? description { get; set; }
        public Task task { get; } = null!;
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deadline { get; set; }
        public bool is_completed { get; set; }
    }
}