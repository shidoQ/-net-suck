using GasStationAPI.Data;
using GasStationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GasStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly GasStationDbContext _context;

        public ManagersController(GasStationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetManagers()
        {
            var managers = await _context.Managers.ToListAsync();
            return Ok(managers);
        }

        [HttpPost]
        public async Task<IActionResult> AddManager([FromBody] Manager manager)
        {
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetManagers), new { id = manager.Id }, manager);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManager(int id, [FromBody] Manager manager)
        {
            if (id != manager.Id)
            {
                return BadRequest();
            }

            _context.Entry(manager).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var manager = await _context.Managers.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }

            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
