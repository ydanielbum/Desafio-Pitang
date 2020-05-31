using System;
using System.Linq;
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
    [Route("api/[controller]")]
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
        public async Task<ActionResult> SignIn(UserLoginDto model)
        {

            try
            {        
               var user = await _userRepository.GetAsync(model.Email, model.Password);

                if (user == null)
                    return NotFound(new  { message = "Invalid e-mail or password", errorCode = StatusCodes.Status404NotFound });

                var result = _mapper.Map<UserLoggedDto>(user);
                result.Token = TokenService.GenerateToken(user);
                if (result.Token != null)
                {
                    user.Last_Login = DateTime.Now;
                    _userRepository.Update(user);
                    await _userRepository.SaveChangesAsync();
                }
                
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

                if (await _userRepository.VerifyEmailExists(user.Email) )
                	return BadRequest(new  { message = "Email Already Exists", errorCode = StatusCodes.Status400BadRequest });

                _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();
               
                return Created("","Success!");
            }
            catch (System.Exception e)
            {               
                return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);               
            }            
        }

        [HttpGet]
        [Route("me")]
        public async Task<ActionResult> Authenticated() 
        {
            bool tokenExpired = false;
                       
            if(!User.Identity.IsAuthenticated)
            {

                bool.TryParse( Response.Headers["Token-Expired"], out tokenExpired );

                if (tokenExpired)
                {
                    return this.StatusCode(StatusCodes.Status401Unauthorized, new  { message = "Unauthorized - Token Expired", errorCode = StatusCodes.Status401Unauthorized });
                }
 
                return this.StatusCode(StatusCodes.Status401Unauthorized, new  { message = "Unauthorized", errorCode = StatusCodes.Status401Unauthorized });
            }

            var user = await _userRepository.GetByEmailAsync(User.Identity.Name);
            var result = _mapper.Map<UserDto>(user);

            return Ok(result);
        }
    }
}