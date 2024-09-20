namespace GasStationAPI.Models
{
    public class EmployeeDTO
    {
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // ควรจะเข้ารหัสก่อนบันทึก
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
    }
}
