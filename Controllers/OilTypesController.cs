using GasStationAPI.Data;
using GasStationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GasStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OilTypesController : ControllerBase
    {
        private readonly GasStationDbContext _context;

        public OilTypesController(GasStationDbContext context)
        {
            _context = context;
        }

        // GET: api/OilTypes
        [HttpGet]
        public async Task<IActionResult> GetOilTypes()
        {
            var oilTypes = await _context.OilTypes
                .Select(ot => new {
                    ot.Id,
                    ot.Name,
                    ot.Status,
                    Price = _context.OilPrices
                                .Where(op => op.OilId == ot.Id)
                                .OrderByDescending(op => op.Date)
                                .Select(op => (decimal?)op.Price)
                                .FirstOrDefault() ?? ot.Price,
                    LastUpdate = _context.OilPrices
                                  .Where(op => op.OilId == ot.Id)
                                  .OrderByDescending(op => op.Date)
                                  .Select(op => op.Date.ToString("yyyy-MM-dd"))
                                  .FirstOrDefault() ?? "N/A"
                })
                .ToListAsync();

            return Ok(oilTypes);
        }

        // POST: api/OilTypes
        [HttpPost]
        public async Task<IActionResult> AddOilType([FromBody] OilType oilType)
        {
            _context.OilTypes.Add(oilType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOilTypes", new { id = oilType.Id }, oilType);
        }

        [HttpGet("prices")]
        public async Task<IActionResult> GetOilPrices()
        {
            var oilPrices = await _context.OilTypes
                .Select(ot => new 
                {
                    ot.Name,
                    Price = _context.OilPrices
                            .Where(op => op.OilId == ot.Id)
                            .OrderByDescending(op => op.Date)
                            .Select(op => (decimal?)op.Price)
                            .FirstOrDefault() ?? ot.Price
                })
                .ToListAsync();
            return Ok(oilPrices);
        }

        // DELETE: api/OilTypes/5
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

            return NoContent();
        }

        // PUT: api/OilTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOilType(int id, [FromBody] OilType oilType)
        {
            if (id != oilType.Id)
            {
                return BadRequest();
            }

            _context.Entry(oilType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.OilTypes.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
