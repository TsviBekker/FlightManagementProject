using back_end_api.Context;
using back_end_api.ControlCenter;
using back_end_api.Repository.Models;
using back_end_api.Services.Logic;

namespace back_end_api.Services.Simulation.Mover
{
    public class FlightMover : IFlightMover
    {
        private IControlCenter controlCenter;
        public FlightMover()
        {
            controlCenter = new ControlCenter.ControlCenter(new FlightsDbContext());
        }

        public void BeginWork()
        {
            controlCenter = new ControlCenter.ControlCenter(new FlightsDbContext());
        }

        public async Task ReleaseFlightFromAsync(Flight? flight, int stationId)
        {
            //var flight = await controlCenter.Flights.Get(flightId);
            var station = await controlCenter.Stations.Get(stationId);
            if (flight == null || station == null) return;
            //What needs to be done???
            //1. update departing flight to complete and update its time
            var df = await controlCenter.DepartingFlights.GetByStationAndFlight(stationId, flight.FlightId);
            if (df == null)
                throw new Exception("No DF found"); //change this later
            df.HasDeparted = true;
            df.DepartedAt = DateTime.Now;
            controlCenter.DepartingFlights.Update(df);
            //2. remove flight from current station
            station.FlightId = null;
            //flight.StationId = FlightRandomizer.GenerateNextStation(controlCenter); //++++++++++++++++
            flight.StationId++;

            controlCenter.Stations.Update(station);
            controlCenter.Flights.Update(flight);
            //3. save changes
            await controlCenter.Complete();
        }

        public async Task SendFlightToAsync(Flight? flight, int stationId)
        {
            //var flight = await controlCenter.Flights.Get(flightId);
            var station = await controlCenter.Stations.Get(stationId);
            if (flight == null || station == null) return;
            //What needs to be done???
            //1. create new arriving flight to next station
            await controlCenter.ArrivingFlights.Add(new ArrivingFlight()
            {
                FlightId = flight.FlightId,
                StationId = stationId,
            });
            ////2. update next station's flight ?!?!?! (should it be done now or once plane arrives)
            station.FlightId = flight.FlightId;
            flight.StationId = stationId;
            controlCenter.Flights.Update(flight);
            controlCenter.Stations.Update(station);
            //3. save changes
            await controlCenter.Complete();
        }

        public async Task RegisterFlightAtAsync(Flight? flight, int stationId)
        {
            //var flight = await controlCenter.Flights.Get(flightId);
            var station = await controlCenter.Stations.Get(stationId);
            if (flight == null || station == null) return;
            //What needs to be done???
            //1. update arriving flight to done and update datetime
            var af = await controlCenter.ArrivingFlights.GetByStationAndFlight(stationId, flight.FlightId);
            if (af == null)
                throw new Exception("NO AF FOUND"); //HANDLE EXCEPTIONS LATER
            af.HasArrived = true;
            af.ArrivedAt = DateTime.Now;
            controlCenter.ArrivingFlights.Update(af);

            //2. update flight.station and station.flight
            flight.StationId = stationId;
            station.FlightId = flight.FlightId;
            controlCenter.Flights.Update(flight);
            controlCenter.Stations.Update(station);
            //3. create new departing flight
            await controlCenter.DepartingFlights.Add(new DepartingFlight()
            {
                FlightId = flight.FlightId,
                StationId = stationId,
                HasDeparted = false
            });
            //4. save changes
            await controlCenter.Complete();
            await Task.Delay(flight.PrepTime * 1000);
        }
    }
}
