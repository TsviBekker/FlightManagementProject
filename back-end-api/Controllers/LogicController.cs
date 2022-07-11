using back_end_api.ControlCenter;
using back_end_api.Dtos.Logic;
using back_end_api.Repository.Models;
using back_end_api.Services.FlightManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogicController : ControllerBase
    {
        private readonly IControlCenter controlCenter;
        private readonly IFlightManager flightManager;
        //Ctor
        public LogicController(IControlCenter controlCenter, IFlightManager flightManager)
        {
            this.controlCenter = controlCenter;
            this.flightManager = flightManager;
        }
        [HttpGet("stations-overview")]
        public async Task<ActionResult<IEnumerable<StationOverviewDto>>> GetStationsOverview()
        {
            var stations = await controlCenter.Stations.GetAll();
            var lst = new List<StationOverviewDto>();
            foreach (var station in stations)
            {
                var flight = station.FlightId != null ? await controlCenter.Flights.Get((int)station.FlightId) : null;
                lst.Add(new StationOverviewDto()
                {
                    StationId = station.StationId,
                    Name = station.Name,
                    IsAvailable = station.FlightId == null ? true : false,
                    FlightInStation = flight,
                });
            }
            return lst;
        }


        [HttpGet("send-away/{flightId:int}&{stationId:int}")]
        public ActionResult SendFlightAway(int flightId, int stationId)
        {
            if (flightId == 0 || stationId == 0) return BadRequest();
            else
            {
                flightManager.SendFlight(flightId, stationId, stationId + 1);
                return Ok();
            }
        }

        [HttpGet("get-station-history/{stationId:int}")]
        public async Task<ActionResult<IEnumerable<StationHistoryDto>>> GetStationHistory(int stationId)
        {
            if(stationId == 0) return BadRequest();
            //var lst = new List<StationHistoryDto>();
            //var dep = (await  controlCenter.DepartingFlights.GetAll()).Where(f => f.StationId == stationId);
            //var arr = (await controlCenter.ArrivingFlights.GetAll()).Where(f => f.StationId == stationId);
            return NoContent();
        }
    }
}
