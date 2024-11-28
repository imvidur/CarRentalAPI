using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CarRentalSystemAPI.Models;
using CarRentalSystemAPI.Repository;
using Microsoft.IdentityModel.Tokens;


namespace CarRentalSystemAPI.Services
{
    public class UserService: IuserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtKey;
        public UserService(IUserRepository userRepository, string jwtKey)
        {
            _userRepository = userRepository;
            _jwtKey= jwtKey;
        }

        public async Task<bool> RegisterUser(User user)
        {
            var existingUser = await _userRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                return false;
            }

            await _userRepository.AddUser(user);
            return true;
        }

        public async Task<string> AuthenticateUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null || user.Password != password)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
