using back_end_api.Context;
using back_end_api.Repository.Generic;
using back_end_api.Repository.Models;

namespace back_end_api.Repository.StationRepo
{
    public class StationRepository : GenericRepository<Station>, IStationRepository
    {
        public StationRepository(FlightsDbContext context) : base(context)
        {
        }

        public IEnumerable<Station> GetAvailable()
        {
            return context.Stations.Where(s => s.FlightId == null);
        }
    }
}
