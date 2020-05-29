using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Users.API.Models;

namespace Users.API.Dtos
{
    public class UserSignUpDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public List<Phone> Phones { get; set; }
    }
}