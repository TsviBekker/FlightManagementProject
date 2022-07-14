using back_end_api.Repository.Models;

namespace back_end_api.Services.Simulation.Mover
{
    public interface IFlightMover
    {
        Task ReleaseFlightFromAsync(Flight? flight, int stationId);
        Task SendFlightToAsync(Flight? flight, int stationId);
        Task RegisterFlightAtAsync(Flight? flight, int stationId);
        void BeginWork();
    }
}
