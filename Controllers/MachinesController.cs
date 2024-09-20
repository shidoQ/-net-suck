using GasStationAPI.Data;
using GasStationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GasStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private readonly GasStationDbContext _context;

        public MachinesController(GasStationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMachines()
        {
            var machines = await _context.Machines.Include(m => m.OilType).ToListAsync();
            return Ok(machines);
        }

        [HttpPost]
        public async Task<IActionResult> AddMachine([FromBody] Machine machine)
        {
            _context.Machines.Add(machine);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMachines), new { id = machine.Id }, machine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachine(int id, [FromBody] Machine machine)
        {
            if (id != machine.Id)
            {
                return BadRequest();
            }

            _context.Entry(machine).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                return NotFound();
            }

            _context.Machines.Remove(machine);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
