using GasStationAPI.Data;
using GasStationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GasStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TankersController : ControllerBase
    {
        private readonly GasStationDbContext _context;

        public TankersController(GasStationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTankers()
        {
            var tankers = await _context.Tankers.Include(t => t.Supplier).Include(t => t.OilType).ToListAsync();
            return Ok(tankers);
        }

        [HttpGet("quantity")]
          public async Task<IActionResult> GetTotalOilQuantity()
        {
           var totalQuantity = await _context.Tankers.SumAsync(t => t.Quantity);
    return Ok(new { totalQuantity });
         }


        [HttpPost]
        public async Task<IActionResult> AddTanker([FromBody] Tanker tanker)
        {
            _context.Tankers.Add(tanker);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTankers), new { id = tanker.Id }, tanker);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTanker(int id, [FromBody] Tanker tanker)
        {
            if (id != tanker.Id)
            {
                return BadRequest();
            }

            _context.Entry(tanker).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTanker(int id)
        {
            var tanker = await _context.Tankers.FindAsync(id);
            if (tanker == null)
            {
                return NotFound();
            }

            _context.Tankers.Remove(tanker);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
