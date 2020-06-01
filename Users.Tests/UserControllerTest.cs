using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Users.API.Controllers;
using Users.API.Dtos;
using Users.API.Models;
using Users.API.Repositories;
using Xunit;

namespace Users.Tests
{
    public class UserControllerTest
    {   
        AppDbContext _context;
        IUserRepository _userRepository;
        IMapper _mapper;
        UserController _userController;

       public UserControllerTest()
       {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                                .UseInMemoryDatabase("InMemoryDatabase")
                                .Options;

            _context = new AppDbContext(options);
            _userRepository = new UserRepository(_context); 
            _userController = new UserController(_userRepository, _mapper);
       }

       [Fact]
        public void AddUser_Success()
        {
            var user = new User()
            {
                FirstName = "Hello",
                LastName = "World",
                Email = "hello@world.com",
                Password = "hunter2"
            };
            
            _userRepository.Add(user);
            var addedUser = _userRepository.GetByEmailAsync(user.Email);

            Assert.IsType<Task<User>>(addedUser);
        }



       [Fact]
        public void SignIn_ReturnsNotFound()
        {

            var user = new UserLoginDto()
            {
                Email = "hello@world.com",
                Password = "hunter2"
            };

            var response = _userController.SignIn(user);
        
            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        [Fact]
        public void SignUp_ReturnsOkResult()
        {

           var user = new UserSignUpDto()
            {
                FirstName = "Hello",
                LastName = "World",
                Email = "daniel@world.com",
                Password = "hunter2"
            };

            var response = _userController.SignUp(user);
        
            Assert.IsType<ObjectResult>(response.Result);
        }
        
    }
}
