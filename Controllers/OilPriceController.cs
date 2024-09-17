using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GasStationAPI.Data;
using GasStationAPI.Models;

namespace GasStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OilPriceController : ControllerBase
    {
        private readonly GasStationDbContext _context;

        public OilPriceController(GasStationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOilPrice(OilPrice oilPrice)
        {
            _context.OilPrices.Add(oilPrice);

            var oilType = await _context.OilTypes.FindAsync(oilPrice.OilId);
            if (oilType != null)
            {
                oilType.Price = oilPrice.Price;
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
