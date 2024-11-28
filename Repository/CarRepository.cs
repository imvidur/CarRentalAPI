using CarRentalSystemAPI.Data;
using CarRentalSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace CarRentalSystemAPI.Repository
{
    public class CarRepository: ICarRepository
    {
        private readonly CarRentalContext _context;
        public CarRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task AddCar(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        public async Task<Car> GetCarById(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<List<Car>> GetAvailableCars()
        {
            return await _context.Cars.Where(c => c.IsAvailable).ToListAsync();
        }

        public async Task UpdateCar(int id, bool isAvailable)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car!=null)
            {
                car.IsAvailable = isAvailable;
                _context.Cars.Update(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}
