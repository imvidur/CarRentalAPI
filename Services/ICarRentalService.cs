using CarRentalSystemAPI.Models;
namespace CarRentalSystemAPI.Services
{
    public interface ICarRentalService
    {
        Task<bool> RentCar(int carId, int userId);
        Task<bool> CheckCarAvailability(int carId);
    }
}
