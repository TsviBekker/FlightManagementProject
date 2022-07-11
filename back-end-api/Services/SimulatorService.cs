namespace back_end_api.Services
{
    public class SimulatorService : BackgroundService
    {
        private readonly IFlightSimulatorService flightSimulatorService;
        private HttpClient client;
        private ILogger<SimulatorService> logger;
        public SimulatorService(IFlightSimulatorService flightSimulatorService, ILogger<SimulatorService> logger)
        {
            this.flightSimulatorService = flightSimulatorService;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var res = await client.GetAsync("https://localhost:3000", stoppingToken);
            if (res.IsSuccessStatusCode)
            {
                logger.LogInformation($"Front end is UP... Status Code: {res.StatusCode}");
            }
            else
            {
                logger.LogError($"Front end is DOWN... Status Code: {res.StatusCode}");
            }
            //flightSimulatorService.CreateDepartingFlight(stoppingToken);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            var flight = flightSimulatorService.CreateFlight(cancellationToken);
            flightSimulatorService.CreateArrivingFlight(cancellationToken, flight);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
