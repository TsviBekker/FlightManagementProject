using back_end_api.Context;
using back_end_api.ControlCenter;
using back_end_api.Repository.Models;
using back_end_api.Services.Logic;
using back_end_api.Services.Simulation.Mover;

namespace back_end_api.Services.Simulation.Wrokers
{
    public class FlightManager : IFlightManager
    {
        private readonly IControlCenter controlCenter;
        private readonly ILogger<FlightManager> logger;
        private readonly IFlightMover flightMover;
        public FlightManager(IControlCenter controlCenter, ILogger<FlightManager> logger, IFlightMover flightMover)
        {
            this.controlCenter = controlCenter;
            this.logger = logger;
            this.flightMover = flightMover;
        }

        public async Task HandleFlights(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                logger.LogInformation("Handling Flight");
                //var flights = await controlCenter.Flights.GetAll();
                foreach (var flight in await controlCenter.Flights.GetAll())
                {
                    if (flight.PrepTime > 0 && flight.StationId != null)
                    {
                        flight.PrepTime--;
                        controlCenter.Flights.Update(flight);
                        await controlCenter.Complete();
                    }
                    if (flight.PrepTime == 0 && flight.StationId != null)
                    {
                        flight.PrepTime = FlightRandomizer.GeneratePrepTime();
                        Thread thread = new Thread(async () =>
                        {
                            //controlCenter.InstantiateNewContext();
                            flightMover.BeginWork();
                            await flightMover.ReleaseFlightFromAsync(flight, (int)flight.StationId);
                            await flightMover.SendFlightToAsync(flight, flight.StationId.Value);
                            await flightMover.RegisterFlightAtAsync(flight, (int)flight.StationId);

                            //await controlCenter.Dispose();
                        });
                        thread.Start();
                    }
                }
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
