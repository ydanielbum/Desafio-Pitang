using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.API.Dtos;
using Users.API.Models;
using Users.API.Repositories;
using Users.API.Services;

namespace Users.API.Controllers
{
    [ApiController]
    [Route("")]
    public class UserController : ControllerBase
    {
         private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("signIn")]
        public async Task<ActionResult<dynamic>> SignIn(UserLoginDto model)
        {
            try
            {

               var userLogin = _mapper.Map<User>(model);
               var user = await _userRepository.GetAsync(userLogin.Email, userLogin.Password);

                if (user == null)
                    return NotFound(new { message = "Invalid e-mail or password" });

                var result = _mapper.Map<UserLoggedDto>(user);
                result.Token = TokenService.GenerateToken(user);
                
                return Ok(result);
            }
            catch (System.Exception e)
            {               
                return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);               
            }            
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp(UserSignUpDto model)
        {
            try
            {
                var user = _mapper.Map<User>(model);

                _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();
               
                return Created("","Criado com Sucesso!");
               

            }
            catch (System.Exception e)
            {               
                return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);               
            }            
        }

        [HttpGet]
        [Route("me")]
        [Authorize]
        public async Task<ActionResult> Authenticated() 
        {
            var user = await _userRepository.GetByEmailAsync(User.Identity.Name);
            var result = _mapper.Map<UserDto>(user);

            return Ok(result);
        }
    }
}