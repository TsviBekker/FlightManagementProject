using back_end_api.ControlCenter;
using back_end_api.Services.Simulation.Wrokers;

namespace back_end_api.Services.Simulation
{
    public class BackgroundSimulator : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        public BackgroundSimulator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                #region Explanation
                // The problem:
                //  - Singleton services (background service)
                //  - cannot consume Scoped/Transient services via DI (dep injection)
                // The Solution:
                //  - Create a scope inside the singleton service
                //  - Get required service inside the scope
                //  - Do your work there....
                #endregion

                using var scope = serviceProvider.CreateScope();
                var flightManager = scope.ServiceProvider.GetRequiredService<IFlightManager>();

                await flightManager.HandleFlights(stoppingToken);
            }

        }
    }
}
