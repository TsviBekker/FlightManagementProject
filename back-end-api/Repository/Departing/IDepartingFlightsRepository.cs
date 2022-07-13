using back_end_api.Repository.Generic;
using back_end_api.Repository.Models;

namespace back_end_api.Repository.Departing
{
    public interface IDepartingFlightsRepository : IGenericRepository<DepartingFlight>
    {
        Task<DepartingFlight?> GetByStationAndFlight(int stationId, int flightId);
        Task<IEnumerable<DepartingFlight>> GetHistoryByStationId(int stationId);
        Task<IEnumerable<DepartingFlight>?> GetPending();
    }
}
