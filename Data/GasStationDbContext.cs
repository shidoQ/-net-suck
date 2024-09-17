using GasStationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GasStationAPI.Data
{
    public class GasStationDbContext : DbContext
    {
        public GasStationDbContext(DbContextOptions<GasStationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OilType> OilTypes { get; set; }
        public DbSet<OilPrice> OilPrices { get; set; }
    }
}
