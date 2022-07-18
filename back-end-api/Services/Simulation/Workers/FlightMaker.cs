using back_end_api.ControlCenter;
using back_end_api.Repository.Models;
using back_end_api.Services.Logic;

namespace back_end_api.Services.Simulation.Wrokers
{
    public class FlightMaker : IFlightMaker
    {
        private readonly ILogger<FlightMaker> logger;
        private readonly IControlCenter controlCenter;

        public FlightMaker(ILogger<FlightMaker> logger, IControlCenter controlCenter)
        {
            this.logger = logger;
            this.controlCenter = controlCenter;
        }

        public async Task MakeFlight(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                logger.LogInformation("............................Making a new flight");

                Flight flight = new Flight()
                {
                    Code = FlightRandomizer.GenerateCode(),
                    Airline = "EL-AL",
                    PrepTime = FlightRandomizer.GeneratePrepTime(),
                    StationId = 1
                };

                await controlCenter.Flights.Add(flight);
                var flights = await controlCenter.Flights.GetAll();
                var id = 0;
                if (flights.Any())
                    id = flights.Last().FlightId;

                await controlCenter.DepartingFlights.Add(new DepartingFlight()
                {
                    FlightId = id + 1,
                    StationId = 1,
                });

                var station = await controlCenter.Stations.Get(1);
                station!.FlightId = id + 1;

                await controlCenter.Complete();

                await Task.Delay(30 * 1000, cancellationToken);
            }
        }
    }
}
