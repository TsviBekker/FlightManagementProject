using back_end_api.Context;
using back_end_api.Repository.Generic;
using back_end_api.Repository.Models;

namespace back_end_api.Repository.Departing
{
    public class DepartingFlightsRepository : GenericRepository<DepartingFlight>, IDepartingFlightsRepository
    {
        public DepartingFlightsRepository(FlightsDbContext context) : base(context)
        {
        }
    }
}
