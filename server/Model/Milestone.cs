using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Model
{
    public class Milestone
    {
        [Key]
        public Guid id { get; set; }
        [ForeignKey("Task")]
        public Guid task_id { get; set; }
        [Required]
        public string title { get; set; } = null!;
        public string? description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deadline { get; set; }
        [Required]
        public bool is_completed { get; set; }
    }
}