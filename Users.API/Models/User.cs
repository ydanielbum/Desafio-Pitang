using System;
using System.Collections.Generic;

namespace Users.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Phone> Phones { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Last_Login { get; set; }
        public string Token { get; set; }
    }
}