using back_end_api.Services.Simulation.Wrokers;

namespace back_end_api.Services.Simulation
{
    public class BackgroundFlightMaker: BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<BackgroundSimulator> logger;
        public BackgroundFlightMaker(IServiceProvider serviceProvider, ILogger<BackgroundSimulator> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var flightMaker = scope.ServiceProvider.GetRequiredService<IFlightMaker>();

                    await flightMaker.MakeFlight(stoppingToken);
                }
            }
        }
    }
}
