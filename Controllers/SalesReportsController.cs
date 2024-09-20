using GasStationAPI.Data;
using GasStationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GasStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesReportsController : ControllerBase
    {
        private readonly GasStationDbContext _context;

        public SalesReportsController(GasStationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddSalesReport([FromBody] SalesReportDTO report)
        {
            var machineNumber = report.MachineNumber;
            var oilId = report.OilId;
            var price = report.Price;
            var userId = report.UserId;

            try
            {
                var oilPrice = await _context.OilPrices
                    .Where(op => op.OilId == oilId)
                    .OrderByDescending(op => op.Date)
                    .Select(op => op.Price)
                    .FirstOrDefaultAsync();

                if (oilPrice == 0)
                {
                    return BadRequest(new { message = "Oil price not found" });
                }

                var quantity = (decimal)price / oilPrice;

                var salesReport = new SalesReport
                {
                    MachineNumber = machineNumber,
                    OilId = oilId,
                    Price = price,
                    Quantity = (float)quantity, // Convert decimal to float
                    Date = DateTime.UtcNow,
                    UserId = userId
                };
                _context.SalesReports.Add(salesReport);
                await _context.SaveChangesAsync();

                var tanker = await _context.Tankers
                    .Where(t => t.OilId == oilId && t.Quantity >= (float)quantity) // Convert quantity to float for comparison
                    .FirstOrDefaultAsync();

                if (tanker != null)
                {
                    tanker.Quantity -= (float)quantity; // Ensure type consistency
                    await _context.SaveChangesAsync();
                }

                return CreatedAtAction("GetSalesReports", new { id = salesReport.Id }, salesReport);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding sale report or updating tanker: ", ex);
                return StatusCode(500, new { message = "Server error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesReports()
        {
            var salesReports = await _context.SalesReports.Include(sr => sr.OilType).ToListAsync();
            return Ok(salesReports);
        }
    }
}
