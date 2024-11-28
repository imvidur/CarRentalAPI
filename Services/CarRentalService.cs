using CarRentalSystemAPI.Repository;

namespace CarRentalSystemAPI.Services
{
    public class CarRentalService:ICarRentalService
    {
        private readonly ICarRepository _carRepository;
        public CarRentalService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task<bool> RentCar(int carId, int userId)
        {
            var car = await _carRepository.GetCarById(carId);
            if (car == null || !car.IsAvailable)
            {
                return false;
            }

            // Mark the car as unavailable
            await _carRepository.UpdateCar(carId, false);
            return true;
        }

        public async Task<bool> CheckCarAvailability(int carId)
        {
            var car = await _carRepository.GetCarById(carId);
            // If the car is found (car != null) and it’s available (car.IsAvailable) return true
            return car != null && car.IsAvailable;
        }
    }
}
