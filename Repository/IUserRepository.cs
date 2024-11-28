using CarRentalSystemAPI.Models;
using System.Threading.Tasks;

namespace CarRentalSystemAPI.Repository
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
    }
}
