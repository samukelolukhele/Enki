using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Model
{
    public class DayPlan
    {
        [Key]
        public Guid id { get; set; }
        [ForeignKey("User")]
        public Guid user_id { get; set; }
        public string title { get; set; } = null!;
        public string? description { get; set; }
        public ICollection<Task> tasks { get; } = new List<Model.Task>();
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deadline { get; set; }
        [Required]
        public bool is_completed { get; set; }
    }
}