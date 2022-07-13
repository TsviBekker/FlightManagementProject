using back_end_api.ControlCenter;
using back_end_api.Repository.Models;
using back_end_api.Services.Logic;

namespace back_end_api.Services.Simulation.Wrokers
{
    public class FlightManager : IFlightManager
    {
        private readonly IControlCenter controlCenter;
        private readonly ILogger<FlightManager> logger;
        public FlightManager(IControlCenter controlCenter, ILogger<FlightManager> logger)
        {
            this.controlCenter = controlCenter;
            this.logger = logger;
        }

        public async Task ReleaseFlightFromAsync(int flightId, int stationId)
        {
            var flight = await controlCenter.Flights.Get(flightId);
            var station = await controlCenter.Stations.Get(stationId);
            if (flight == null || station == null) return;
            //What needs to be done???
            //1. update departing flight to complete and update its time
            var df = await controlCenter.DepartingFlights.GetByStationAndFlight(stationId, flightId);
            if (df == null) throw new Exception("No DF found"); //change this later
            df.HasDeparted = true;
            df.DepartedAt = DateTime.Now;
            controlCenter.DepartingFlights.Update(df);
            //2. remove flight from current station
            station.FlightId = null;
            flight.StationId = flight.StationId + 1;
            controlCenter.Stations.Update(station);
            controlCenter.Flights.Update(flight);
            //3. save changes
            await controlCenter.Complete();
        }

        public async Task SendFlightToAsync(int flightId, int stationId)
        {
            var flight = await controlCenter.Flights.Get(flightId);
            var station = await controlCenter.Stations.Get(stationId);
            if (flight == null || station == null) return;
            //What needs to be done???
            //1. create new arriving flight to next station
            await controlCenter.ArrivingFlights.Add(new ArrivingFlight()
            {
                FlightId = flightId,
                StationId = stationId,
            });
            ////2. update next station's flight ?!?!?! (should it be done now or once plane arrives)
            station.FlightId = flightId;
            flight.StationId = stationId;
            controlCenter.Flights.Update(flight);
            controlCenter.Stations.Update(station);
            //3. save changes
            await controlCenter.Complete();
        }

        public async Task RegisterFlightAtAsync(int flightId, int stationId)
        {
            var flight = await controlCenter.Flights.Get(flightId);
            var station = await controlCenter.Stations.Get(stationId);
            if (flight == null || station == null) return;
            //What needs to be done???
            //1. update arriving flight to done and update datetime
            var af = await controlCenter.ArrivingFlights.GetByStationAndFlight(stationId, flightId);
            if (af == null) throw new Exception("NO AF FOUND"); //HANDLE EXCEPTIONS LATER
            af.HasArrived = true;
            af.ArrivedAt = DateTime.Now;
            controlCenter.ArrivingFlights.Update(af);
            //2. update flight.station and station.flight
            flight.StationId = stationId;
            station.FlightId = flightId;
            controlCenter.Flights.Update(flight);
            controlCenter.Stations.Update(station);
            //3. create new departing flight
            await controlCenter.DepartingFlights.Add(new DepartingFlight()
            {
                FlightId = flightId,
                StationId = stationId,
                HasDeparted = false
            });
            //4. save changes
            await controlCenter.Complete();
            await Task.Delay(flight.PrepTime * 1000);
        }

        public async Task HandleFlights(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {

                logger.LogInformation("Handling Flight");
                var flights = await controlCenter.Flights.GetAll();
                foreach (var flight in flights)
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
                        await ReleaseFlightFromAsync(flight.FlightId, (int)flight.StationId);
                        await SendFlightToAsync(flight.FlightId, (int)flight.StationId);
                        await RegisterFlightAtAsync(flight.FlightId, (int)flight.StationId);
                    }
                }
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
