using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dto
{
    public class MilestoneDto
    {
        public Guid id { get; set; }
        public string title { get; set; } = null!;
        public string? description { get; set; }
        public bool is_completed { get; set; }
        public DateTime deadline { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
    }

    public class CreateMilestoneDto
    {
        public Guid id { get; set; }
        public Guid task_id { get; set; }
        public string title { get; set; } = null!;
        public string? description { get; set; }
        public bool is_completed { get; set; }
        public DateTime deadline { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
    }
}