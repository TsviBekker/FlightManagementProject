using back_end_api.Repository.Generic;
using back_end_api.Repository.Models;

namespace back_end_api.Repository.Arriving
{
    public interface IArrivingFlightsRepository : IGenericRepository<ArrivingFlight>
    {
        Task<ArrivingFlight?> GetByStationAndFlight(int stationId, int flightId);
    }
}
