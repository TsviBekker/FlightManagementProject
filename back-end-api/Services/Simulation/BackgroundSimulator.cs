using back_end_api.ControlCenter;
using back_end_api.Services.Simulation.Wrokers;

namespace back_end_api.Services.Simulation
{
    public class BackgroundSimulator : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<BackgroundSimulator> logger;
        public BackgroundSimulator(IServiceProvider serviceProvider, ILogger<BackgroundSimulator> logger)
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
                    var flightManager = scope.ServiceProvider.GetRequiredService<IFlightManager>();

                    await flightManager.HandleFlights(stoppingToken);
                }
            }

        }
    }
}
