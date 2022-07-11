using back_end_api.ControlCenter;
using back_end_api.Repository.Models;
using back_end_api.Services.Logic;

namespace back_end_api.Services
{
    public class FlightSimulatorService : IFlightSimulatorService
    {
        private readonly ILogger<FlightSimulatorService> logger;

        public FlightSimulatorService(ILogger<FlightSimulatorService> logger)
        {
            this.logger = logger;
        }

        public async Task CreateArrivingFlight(CancellationToken cancellationToken, Flight flight)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                logger.LogInformation("CreateArrivingFlight INVOKED");
                await Task.Delay(1000 * 60 * 10);
            }
        }

        public async Task CreateDepartingFlight(CancellationToken cancellationToken, Flight flight)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                logger.LogInformation("CreateArrivingFlight INVOKED");
                await Task.Delay(1000 * 2);
            }
        }

        public Flight CreateFlight(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return null;
            logger.LogInformation("Creating a new flight....");
            var flight = new Flight()
            {
                Airline = "EL-AL",
                Code = FlightRandomizer.GenerateCode(),
                PrepTime = FlightRandomizer.GeneratePrepTime(),
            };
            return flight;

        }
    }
}
