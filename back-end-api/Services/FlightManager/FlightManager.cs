using back_end_api.ControlCenter;

namespace back_end_api.Services.FlightManager
{
    public class FlightManager : IFlightManager
    {
        private readonly IControlCenter controlCenter;
        public FlightManager(IControlCenter controlCenter)
        {
            this.controlCenter = controlCenter;
        }
        public async void ReceiveFlight(int flightId, int currentStationId, int prevStationId)
        {
            var flight = await controlCenter.Flights.Get(flightId);
            var currStation = await controlCenter.Stations.Get(currentStationId);
            var prevStation = await controlCenter.Stations.Get(prevStationId);
            //What needs to be done?
            //1. register flight in station
            currStation.FlightId = flightId;
            //2. remove flight from prev station (if there was a prev station)
            //3. set arriving flight to be done
            if (prevStation != null)
            {
                prevStation.FlightId = null;
                var arrivingFlight = await controlCenter.ArrivingFlights.GetByStationAndFlight(prevStationId, flightId);
                arrivingFlight!.ArrivedAt = DateTime.Now;
                arrivingFlight.HasArrived = true;
            }
            //4.Save changes
            await controlCenter.Complete();
            //5.* start timer for prep time (task.delay) ?? is it done here or client???
            //await Task.Delay(flight.PrepTime * 1000);
            //SendFlight(flightId, currentStationId, currentStationId + 1);
        }


        public async void SendFlight(int flightId, int currentStationId, int nextStationId = 0)
        {
            var flight = await controlCenter.Flights.Get(flightId);
            var currStation = await controlCenter.Stations.Get(currentStationId);
            //What needs to be done?
            //1. remove flight from current station
            currStation.FlightId = null;
            controlCenter.Stations.Update(currStation);

            //2. set the departing flight to done (departure finished)
            var departingFlight = await controlCenter.DepartingFlights.GetByStationAndFlight(currentStationId, flightId);
            if (departingFlight != null)
            {
                departingFlight.HasDeparted = true;
                departingFlight.DepartedAt = DateTime.Now;
                controlCenter.DepartingFlights.Update(departingFlight);
            }

            //3. make a new arriving flight -> to next station if there is a next station
            if (nextStationId == 0) return;
            var nextStation = await controlCenter.Stations.Get(nextStationId);
            nextStation!.FlightId = flightId;
            await controlCenter.ArrivingFlights.Add(new Repository.Models.ArrivingFlight()
            {
                FlightId = flightId,
                HasArrived = false,
                StationId = nextStationId,
            });
            //4. save changes
            await controlCenter.Complete();
        }
    }
}
