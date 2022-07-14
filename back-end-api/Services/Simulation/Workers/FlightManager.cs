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
        //private readonly IFlightMover flightMover;
        public FlightManager(IControlCenter controlCenter, ILogger<FlightManager> logger/*, IFlightMover flightMover*/)
        {
            this.controlCenter = controlCenter;
            this.logger = logger;
            //this.flightMover = flightMover;
        }

        public async Task HandleFlights(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                logger.LogInformation("Handling Flight");
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
                        #region Explaining a problem:
                        // The problem:
                        //  - asnc/await methods take a long time to complete...
                        //  - because they run on the same thread
                        // Solution:
                        //  - using a thread 
                        // The problem:
                        //  - DbContext is not thread safe
                        // The solution:
                        //  - instatntiate a new DbContext in each thread
                        //  - by calling the constructor method or mover class
                        #endregion

                        flight.PrepTime = FlightRandomizer.GeneratePrepTime();
                        Thread thread = new Thread(async () =>
                        {
                            var flightMover = new FlightMover();

                            await flightMover.ReleaseFlightFromAsync(flight, (int)flight.StationId);
                            await flightMover.SendFlightToAsync(flight, flight.StationId.Value);
                            await flightMover.RegisterFlightAtAsync(flight, (int)flight.StationId);
                        });
                        thread.Start();
                    }
                }
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
