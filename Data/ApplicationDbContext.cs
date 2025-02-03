using System.Drawing;
using Dashboard_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Dashboard_Management.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<EnergyConsumption> EnergyConsumptions { get; set; }
        public DbSet<OccupancyData> OccupancyData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>().ToTable("buildings");
            modelBuilder.Entity<Floor>().ToTable("floors");
            modelBuilder.Entity<EnergyConsumption>().ToTable("energy_consumption");
            modelBuilder.Entity<OccupancyData>().ToTable("occupancy_data");
        }
    }
}
