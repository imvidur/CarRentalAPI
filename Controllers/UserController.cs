using Microsoft.AspNetCore.Mvc;
using CarRentalSystemAPI.Models;
using CarRentalSystemAPI.Services;
using CarRentalSystemAPI.Models;
using CarRentalSystemAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IuserService _userService;

    public UserController(IuserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] User user)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var success = await _userService.RegisterUser(user);
        if (!success) return BadRequest("User already exists.");

        return Ok("User registered successfully.");
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await _userService.AuthenticateUser(request.Email, request.Password);

        if (token == null)
            return Unauthorized("Invalid email or password.");

        return Ok(new { Token = token });
    }


}
