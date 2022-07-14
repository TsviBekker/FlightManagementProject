namespace back_end_api.Services.Simulation.Wrokers
{
    public interface IFlightMaker
    {
        Task MakeFlight(CancellationToken cancellationToken);
    }
}
