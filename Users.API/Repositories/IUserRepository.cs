using System.Threading.Tasks;
using Users.API.Models;

namespace Users.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string email, string password);
    }
}