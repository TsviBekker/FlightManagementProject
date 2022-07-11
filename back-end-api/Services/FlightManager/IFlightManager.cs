namespace back_end_api.Services.FlightManager
{
    public interface IFlightManager
    {
        void SendFlight(int flightId, int currentStationId, int nextStationId);
    }
}
