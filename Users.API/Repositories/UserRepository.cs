using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users.API.Models;

namespace Users.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

         public void Add<User>(User user)
        {
            _context.Add(user);
        }
        public async Task<User> GetAsync(string email, string password)
        {
            return await _context.Users.Where(x => x.Email.ToLower() == email.ToLower() && x.Password == password).FirstOrDefaultAsync();                          
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();                     
        }

        public async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync()) > 0;
        }

       
    }
}