using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Model;

namespace server.Dto
{
    public class DayPlanDto
    {
        public Guid id { get; set; }
        public string title { get; set; } = null!;
        public string? description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deadline { get; set; }
        public bool is_completed { get; set; }
    }

    public class CreateDayPlanDto
    {
        public Guid id { get; set; }
        public Guid user_id { get; set; }
        public string title { get; set; } = null!;
        public string? description { get; set; } = null!;
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deadline { get; set; }
        public bool is_completed { get; set; }
    }
}