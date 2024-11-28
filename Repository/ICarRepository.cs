using CarRentalSystemAPI.Models;
namespace CarRentalSystemAPI.Repository
{
    public interface ICarRepository
    {
        Task AddCar(Car car);
        Task<Car> GetCarById(int id);
        Task<List<Car>> GetAvailableCars();

        Task UpdateCar(int id, bool isAvailable);
    }
}
