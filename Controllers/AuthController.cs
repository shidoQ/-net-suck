using GasStationAPI.Models;
using GasStationAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GasStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var authResponse = _authService.Authenticate(loginDTO);

            if (authResponse == null) return Unauthorized();

            return Ok(authResponse);
        }
    }
}
