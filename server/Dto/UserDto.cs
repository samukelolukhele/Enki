using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dto
{
    public class GetUserDto
    {
        public Guid id { get; set; }
        public string email { get; set; } = string.Empty;
        public string fName { get; set; } = string.Empty;
        public DateTime created_at { get; set; }
    }
    public class CreateUserDto
    {
        public Guid id { get; set; }
        public string email { get; set; } = null!;
        public string fName { get; set; } = null!;
        public string lName { get; set; } = null!;
        public string password { get; set; } = null!;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }

    public class UpdateUserDto
    {
        public string? email { get; set; }
        public string? fName { get; set; }
        public string? lName { get; set; }
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }



}