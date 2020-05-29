using System.Collections.Generic;
using Users.API.Models;

namespace Users.API.Dtos
{
    public class UserDto
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<PhoneDto> Phones { get; set; }
        public string Created_at { get; set; }
        public string Last_login { get; set; }

    }
}