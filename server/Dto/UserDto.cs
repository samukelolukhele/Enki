using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dto
{
    public class UserDto
    {
        public Guid id { get; set; }
        public string email { get; set; } = null!;
        public string fName { get; set; } = null!;
        public string lName { get; set; } = null!;
        public string password { get; set; } = null!;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }
}