using back_end_api.Context;
using back_end_api.Repository.Generic;
using back_end_api.Repository.Models;

namespace back_end_api.Repository.Arriving
{
    public class ArrivingFlightsRepository : GenericRepository<ArrivingFlight>, IArrivingFlightsRepository
    {
        public ArrivingFlightsRepository(FlightsDbContext context) : base(context)
        {
        }
    }
}
