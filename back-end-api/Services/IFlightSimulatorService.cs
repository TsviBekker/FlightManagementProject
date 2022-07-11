using back_end_api.Repository.Models;

namespace back_end_api.Services
{
    public interface IFlightSimulatorService
    {
        Task CreateArrivingFlight(CancellationToken cancellationToken, Flight flight);
        Task CreateDepartingFlight(CancellationToken cancellationToken, Flight flight);
        Flight CreateFlight(CancellationToken cancellationToken);
    }
}
