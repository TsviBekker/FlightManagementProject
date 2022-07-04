using back_end_api.Context;
using back_end_api.Repository.Generic;
using back_end_api.Repository.Models;

namespace back_end_api.Repository.FlightRepo
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        public FlightRepository(FlightsDbContext context) : base(context)
        {
        }
    }
}
