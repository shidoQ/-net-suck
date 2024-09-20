using Microsoft.EntityFrameworkCore;
using GasStationAPI.Models;

namespace GasStationAPI.Data
{
    public class GasStationDbContext : DbContext
    {
        public GasStationDbContext(DbContextOptions<GasStationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<OilType> OilTypes { get; set; }
        public DbSet<OilPrice> OilPrices { get; set; } // Add this line
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<SalesReport> SalesReports { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Tanker> Tankers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add your model configurations here
        }
    }
}
