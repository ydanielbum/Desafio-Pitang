using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public void Update<User>(User user)
        {
            _context.Update(user);
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
            var AddedEntities = _context.ChangeTracker.Entries<User>().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("Created_At").CurrentValue = DateTime.Now;
                E.Property("Last_Login").CurrentValue = DateTime.Now;
            });

             
           return (await _context.SaveChangesAsync()) > 0;
        }


        public async Task<bool> VerifyEmailExists(string email)
        {
            var emailExists = await _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if (emailExists != null){
                return true;
            }

            return false;
        }   
    }
}