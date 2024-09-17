using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GasStationAPI.Data;
using GasStationAPI.Models;

namespace GasStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OilTypeController : ControllerBase
    {
        private readonly GasStationDbContext _context;

        public OilTypeController(GasStationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOilTypes()
        {
            var oilTypes = await _context.OilTypes.ToListAsync();
            return Ok(oilTypes);
        }

        [HttpPost]
        public async Task<IActionResult> AddOilType(OilType oilType)
        {
            _context.OilTypes.Add(oilType);
            await _context.SaveChangesAsync();
            return Ok(oilType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOilType(int id, OilType updatedOilType)
        {
            var oilType = await _context.OilTypes.FindAsync(id);
            if (oilType == null)
            {
                return NotFound();
            }

            oilType.Name = updatedOilType.Name;
            oilType.Price = updatedOilType.Price;

            await _context.SaveChangesAsync();
            return Ok(oilType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOilType(int id)
        {
            var oilType = await _context.OilTypes.FindAsync(id);
            if (oilType == null)
            {
                return NotFound();
            }

            _context.OilTypes.Remove(oilType);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
