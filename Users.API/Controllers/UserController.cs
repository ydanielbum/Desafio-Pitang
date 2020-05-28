using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Users.API.Models;
using Users.API.Repositories;
using Users.API.Services;

namespace Users.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
         private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(User model)
        {
            try
            {
                // Get the user
                var user = await _userRepository.GetAsync(model.Email, model.Password);

                // Verifica se o usuário existe
                if (user == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                // Gera o Token
                var token = TokenService.GenerateToken(user);

                // Oculta a senha
                user.Password = "";
                
                // Retorna os dados
                return new
                {
                    user = user,
                    token = token
                };
            }
            catch (System.Exception)
            {               
                throw;
            }
            
        }
    }
}