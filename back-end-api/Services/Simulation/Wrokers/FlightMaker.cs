using back_end_api.Context;
using back_end_api.ControlCenter;
using back_end_api.Repository.Models;
using back_end_api.Services.Logic;

namespace back_end_api.Services.Simulation.Wrokers
{
    public class FlightMaker : IFlightMaker
    {
        private readonly ILogger<FlightMaker> logger;
        private readonly IControlCenter controlCenter;
        private readonly FlightsDbContext context;

        public FlightMaker(ILogger<FlightMaker> logger, IControlCenter controlCenter, FlightsDbContext context)
        {
            this.logger = logger;
            this.controlCenter = controlCenter;
            this.context = context;
        }

        public async Task MakeFlight(CancellationToken cancellationToken)
        {
            //await Task.Yield();

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(30 * 1000, cancellationToken);
                logger.LogInformation("Make_Flight Invoked......................");

                Flight flight = new Flight()
                {
                    Code = FlightRandomizer.GenerateCode(),
                    Airline = "EL-AL",
                    PrepTime = FlightRandomizer.GeneratePrepTime(),
                    StationId = 1
                };

                await controlCenter.Flights.Add(flight);

                await controlCenter.DepartingFlights.Add(new DepartingFlight()
                {
                    FlightId = 2,
                    StationId = 1,
                });

                var station = await controlCenter.Stations.Get(1);
                station.FlightId = 2;

                await controlCenter.Complete();
            }
        }
    }
}
