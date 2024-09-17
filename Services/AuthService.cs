using GasStationAPI.Data;
using GasStationAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace GasStationAPI.Services
{
    public class AuthService
    {
        private readonly GasStationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(GasStationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public AuthResponse Authenticate(LoginDTO loginDTO)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == loginDTO.Username);

            if (user == null || !VerifyPasswordHash(loginDTO.Password, user.Password))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found in configuration.");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthResponse
            {
                Token = tokenHandler.WriteToken(token),
                Role = user.Role,
                Username = user.Username
            };
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
