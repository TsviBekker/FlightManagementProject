using back_end_api.Context;
using back_end_api.Repository.Generic;
using back_end_api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end_api.Repository.FlightRepo
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        public FlightRepository(FlightsDbContext context) : base(context)
        {
        }

        public async Task<int> GetIdByCode(string code)
        {
            var flight = await context.Flights.FirstOrDefaultAsync(f => f.Code == code);
            if (flight == null) throw new Exception("FLIGHT NOT FOUND");
            return flight.FlightId;
        }
    }
}
