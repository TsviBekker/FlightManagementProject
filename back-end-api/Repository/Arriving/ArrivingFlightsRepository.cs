using back_end_api.Context;
using back_end_api.Repository.Generic;
using back_end_api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end_api.Repository.Arriving
{
    public class ArrivingFlightsRepository : GenericRepository<ArrivingFlight>, IArrivingFlightsRepository
    {
        public ArrivingFlightsRepository(FlightsDbContext context) : base(context)
        {
        }
        public async Task<ArrivingFlight?> GetByStationAndFlight(int stationId, int flightId)
        {
            return await context.ArrivingFlights.FirstOrDefaultAsync(f => f.FlightId == flightId && f.StationId == stationId);
        }
    }
}
