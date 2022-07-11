using back_end_api.ControlCenter;
using back_end_api.Dtos.Logic;
using back_end_api.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace back_end_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessLogicController : ControllerBase
    {
        private readonly IControlCenter controlCenter;
        public BusinessLogicController(IControlCenter controlCenter)
        {
            this.controlCenter = controlCenter;
        }

        [HttpGet("receive-flight-at/{flightId:int}&{stationId:int}")]
        public async Task<IActionResult> ReceiveFlightAt(int flightId, int stationId)
        {
            var flight = await controlCenter.Flights.Get(flightId);
            var station = await controlCenter.Stations.Get(stationId);
            if (flight == null || station == null) return BadRequest();
            //What needs to be done???
            //1. update flight.station and station.flight
            flight.StationId = stationId;
            station.FlightId = flightId;
            controlCenter.Flights.Update(flight);
            controlCenter.Stations.Update(station);
            //2. update arriving flight to done and update datetime
            var af = await controlCenter.ArrivingFlights.GetByStationAndFlight(stationId, flightId);
            if (af == null) throw new Exception("NO AF FOUND");
            af.HasArrived = true;
            af.ArrivedAt = DateTime.Now;
            controlCenter.ArrivingFlights.Update(af);
            //3. create new departing flight
            await controlCenter.DepartingFlights.Add(new DepartingFlight()
            {
                FlightId = flightId,
                StationId = stationId
            });
            //4. save changes
            await controlCenter.Complete();
            return Ok();
        }

        [HttpGet("send-flight-to/{flightId:int}&{stationId:int}")]
        public async Task<IActionResult> SendFlightTo(int flightId, int stationId)
        {
            var flight = await controlCenter.Flights.Get(flightId);
            var station = await controlCenter.Stations.Get(stationId);
            if (flight == null || station == null) return BadRequest();
            //What needs to be done???
            //1. create new arriving flight to next station
            await controlCenter.ArrivingFlights.Add(new ArrivingFlight()
            {
                FlightId = flightId,
                StationId = stationId,
            });
            ////2. update next station's flight ?!?!?! (should it be done now or once plane arrives)
            //station.FlightId = flightId;
            //controlCenter.Stations.Update(station);
            //3. save changes
            await controlCenter.Complete();
            return Ok();
        }

        [HttpGet("release-flight-from/{flightId:int}&{stationId:int}")]
        public async Task<IActionResult> ReleaseFlight(int flightId, int stationId)
        {
            var flight = await controlCenter.Flights.Get(flightId);
            var station = await controlCenter.Stations.Get(stationId);
            if (flight == null || station == null) return BadRequest();
            //What needs to be done???
            //1. remove flight from current station
            station.FlightId = null;
            flight.StationId = flight.StationId + 1;
            controlCenter.Stations.Update(station);
            controlCenter.Flights.Update(flight);
            //2. update departing flight to complete and update its time
            var df = await controlCenter.DepartingFlights.GetByStationAndFlight(stationId, flightId);
            if (df == null) throw new Exception("No DF found"); //change this later
            df.HasDeparted = true;
            df.DepartedAt = DateTime.Now;
            controlCenter.DepartingFlights.Update(df);
            //3. save changes
            await controlCenter.Complete();
            return Ok();
        }
        [HttpGet("stations-overview")]
        public async Task<ActionResult<IEnumerable<StationOverviewDto>>> GetStations()
        {
            var stations = await controlCenter.Stations.GetAll();
            return Ok(stations.Select(async s =>
            {
                return new StationOverviewDto()
                {
                    FlightInStation = s.FlightId != null ? await controlCenter.Flights.Get((int)s.FlightId) : null,
                    IsAvailable = s.FlightId == null,
                    StationId = s.StationId,
                    Name = s.Name
                };
            }).Select(t => t.Result));
        }
    }
}
