namespace GasStationAPI.Models
{
    public class AuthResponse
    {
        public required string Token { get; set; }
        public required string Role { get; set; }
        public required string Username { get; set; }
    }
}
