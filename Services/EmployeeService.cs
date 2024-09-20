using GasStationAPI.Data; // ตรวจสอบว่ามีการใช้คำสั่งนี้
using GasStationAPI.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;


namespace GasStationAPI.Services
{
    public class EmployeeService
    {
        private readonly GasStationDbContext _context;

        public EmployeeService(GasStationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> AddEmployeeAsync(EmployeeDTO employeeDTO)
        {
            var employee = new Employee
            {
                EmployeeName = employeeDTO.EmployeeName,
                Email = employeeDTO.Email,
                Username = employeeDTO.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(employeeDTO.Password), // เข้ารหัสรหัสผ่าน
                PhoneNo = employeeDTO.PhoneNo,
                Address = employeeDTO.Address,
                Gender = employeeDTO.Gender,
                Dob = employeeDTO.Dob
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDTO employeeDTO)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                employee.EmployeeName = employeeDTO.EmployeeName;
                employee.Email = employeeDTO.Email;
                employee.Username = employeeDTO.Username;
                if (!string.IsNullOrEmpty(employeeDTO.Password))
                {
                    employee.Password = BCrypt.Net.BCrypt.HashPassword(employeeDTO.Password);
                }
                employee.PhoneNo = employeeDTO.PhoneNo;
                employee.Address = employeeDTO.Address;
                employee.Gender = employeeDTO.Gender;
                employee.Dob = employeeDTO.Dob;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
