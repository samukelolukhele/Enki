using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dto
{
    public class UserDto
    {
        public int id { get; set; }
        public string email { get; set; } = null!;
        public string fName { get; set; } = null!;
    }
}