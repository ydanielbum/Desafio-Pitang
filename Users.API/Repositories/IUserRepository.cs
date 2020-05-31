using System.Threading.Tasks;
using Users.API.Models;

namespace Users.API.Repositories
{
    public interface IUserRepository
    {
        void Add<User>(User user);
        Task<bool> SaveChangesAsync();
        Task<User> GetAsync(string email, string password);
        Task<User> GetByEmailAsync(string email);
        Task<bool> VerifyEmailExists(string email);
    }
}