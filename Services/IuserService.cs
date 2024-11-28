using CarRentalSystemAPI.Models;

namespace CarRentalSystemAPI.Services
{
    public interface IuserService
    {
        Task<bool> RegisterUser(User user);
        Task<string> AuthenticateUser(string email, string password);
    }
}
