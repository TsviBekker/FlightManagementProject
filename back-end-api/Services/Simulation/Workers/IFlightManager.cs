using back_end_api.Repository.Models;

namespace back_end_api.Services.Simulation.Wrokers
{
    public interface IFlightManager
    {
        Task HandleFlights(CancellationToken token);
    }
}
