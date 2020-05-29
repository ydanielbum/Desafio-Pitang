using AutoMapper;
using Users.API.Dtos;
using Users.API.Models;

namespace Users.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();            
            CreateMap<User, UserLoginDto>().ReverseMap(); 
            CreateMap<User, UserLoggedDto>().ReverseMap();   
            CreateMap<User, UserSignUpDto>().ReverseMap();   
        }
    }
}