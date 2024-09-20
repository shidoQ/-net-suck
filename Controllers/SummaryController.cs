using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GasStationAPI.Data;
using GasStationAPI.Models;
using System.Threading.Tasks;

namespace GasStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly GasStationDbContext _context;

        public SummaryController(GasStationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSummary()
        {
            var managerCount = await _context.Users.CountAsync(u => u.Role == "manager");
            var employeeCount = await _context.Users.CountAsync(u => u.Role == "employee");
            var supplierCount = await _context.Suppliers.CountAsync();
            var machineCount = await _context.Machines.CountAsync();
            var totalIncome = await _context.SalesReports.SumAsync(sr => sr.Price * (decimal)sr.Quantity);

            return Ok(new 
            {
                managerCount,
                employeeCount,
                supplierCount,
                machineCount,
                totalIncome
            });
        }
    }
}
