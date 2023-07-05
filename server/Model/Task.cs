using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Model
{
    public class Task
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("DayPlan")]
        public int day_plan_id { get; set; }
        [Required]
        public string title { get; set; } = null!;
        public string? description { get; set; } = default;
        [Required]
        public bool is_completed { get; set; }
        public DayPlan day_plan { get; } = null!;
        public ICollection<Milestone> milestones { get; } = new List<Milestone>();
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deadline { get; set; }
    }
}