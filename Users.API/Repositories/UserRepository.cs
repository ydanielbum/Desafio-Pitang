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

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetAsync(string email, string password)
        {
            return await _context.Users.Where(x => x.Email.ToLower() == email.ToLower() && x.Password == x.Password).FirstOrDefaultAsync();                          
        }

        public async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync()) > 0;
        }

        Task<User> IUserRepository.GetAsync(string email, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}