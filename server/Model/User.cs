using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Model
{
    public class User
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string email { get; set; } = null!;
        [Required]
        public string password { get; set; } = null!;
        [Required]
        public string fName { get; set; } = null!;
        [Required]
        public string lName { get; set; } = null!;
        public ICollection<DayPlan> day_plans { get; } = new List<DayPlan>();
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}