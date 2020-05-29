using System.ComponentModel.DataAnnotations;

namespace Users.API.Dtos
{
    public class UserLoggedDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
}