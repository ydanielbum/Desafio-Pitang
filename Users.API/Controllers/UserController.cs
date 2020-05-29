using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.API.Models;
using Users.API.Repositories;
using Users.API.Services;

namespace Users.API.Controllers
{
    [ApiController]
    [Route("")]
    public class UserController : ControllerBase
    {
         private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
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


        [HttpGet]
        [Route("me")]
        [Authorize]
        public async Task<ActionResult> Authenticated() 
        {
            var result = await _userRepository.GetByEmailAsync(User.Identity.Name);

            return Ok(result);
        }
    }
}