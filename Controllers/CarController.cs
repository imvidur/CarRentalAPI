using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarRentalSystemAPI.Models;
using CarRentalSystemAPI.Services;
using CarRentalSystemAPI.Models;
using CarRentalSystemAPI.Repository;
using CarRentalSystemAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarRentalService _carRentalService;
    private readonly ICarRepository _carRepository;

    public CarController(ICarRentalService carRentalService, ICarRepository carRepository)
    {
        _carRentalService = carRentalService;
        _carRepository = carRepository;
    }

    [HttpGet]
    [Authorize] 
    public async Task<IActionResult> GetAvailableCars()
    {
        var cars = await _carRepository.GetAvailableCars();
        return Ok(cars);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> AddCar([FromBody] Car car)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _carRepository.AddCar(car);
        return CreatedAtAction(nameof(GetAvailableCars), new { id = car.Id }, car);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> UpdateCar(int id, [FromBody] Car updatedCar)
    {
        var car = await _carRepository.GetCarById(id);
        if (car == null) return NotFound();

        car.Make = updatedCar.Make;
        car.Model = updatedCar.Model;
        car.Year = updatedCar.Year;
        car.PricePerDay = updatedCar.PricePerDay;
        car.IsAvailable = updatedCar.IsAvailable;

        await _carRepository.AddCar(car);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> DeleteCar(int id)
    {
        var car = await _carRepository.GetCarById(id);
        if (car == null) return NotFound();

        await _carRepository.AddCar(car);
        return NoContent();
    }
}
