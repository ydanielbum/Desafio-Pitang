using System;
using System.Collections.Generic;
using Users.API.Models;

namespace Users.API.Dtos
{
    public class UserDto
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<PhoneDto> Phones { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Last_Login { get; set; }

    }
}