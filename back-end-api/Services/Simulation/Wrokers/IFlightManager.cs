namespace back_end_api.Services.Simulation.Wrokers
{
    public interface IFlightManager
    {
        Task ReleaseFlightFromAsync(int flightId, int stationId);
        Task SendFlightToAsync(int flightId, int stationId);
        Task RegisterFlightAtAsync(int flightId, int stationId);
        Task HandleFlights(CancellationToken token);

    }
}
