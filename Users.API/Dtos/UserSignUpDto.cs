using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Users.API.Models;

namespace Users.API.Dtos
{
    public class UserSignUpDto
    {
        [Required (ErrorMessage="Missing Fields!")]
        public string FirstName { get; set; }
        [Required (ErrorMessage="Missing Fields!")]
        public string LastName { get; set; }
        [Required (ErrorMessage="Missing Fields!")]
        [EmailAddress (ErrorMessage="Invalid Fields!")]
        public string Email { get; set; }
        [Required (ErrorMessage="Missing Fields!")]
        public string Password { get; set; }
        public List<Phone> Phones { get; set; }
    }
}