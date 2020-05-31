using System.ComponentModel.DataAnnotations;

namespace Users.API.Dtos
{
    public class UserLoginDto
    {
        [Required (ErrorMessage="Missing Fields!")]
        [EmailAddress (ErrorMessage="Invalid Fields!")]
        public string Email { get; set; }
        [Required (ErrorMessage="Missing Fields!")]
        public string Password { get; set; }
    }
}