using Microsoft.EntityFrameworkCore;
using back_end_api.Repository.Models;

namespace back_end_api.Context
{
    public partial class FlightsDbContext : DbContext
    {
        public FlightsDbContext()
        {
        }

        public FlightsDbContext(DbContextOptions<FlightsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArrivingFlight> ArrivingFlights { get; set; } = null!;
        public virtual DbSet<DepartingFlight> DepartingFlights { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Station> Stations { get; set; } = null!;

        //This is needed because new instances of DbContext will be created later
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FlightManagement;Integrated Security=True");
        }
    }
}
