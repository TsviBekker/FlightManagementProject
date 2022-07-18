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
        /// <summary>
        /// Get a snapshot of the airport stations
        /// </summary>
        /// <returns>Collection of StationOverviewDto</returns>
        [HttpGet("stations-overview")]
        public async Task<ActionResult<IEnumerable<StationOverviewDto>>> GetStations()
        {
            var stations = await controlCenter.Stations.GetAll();

            //.Select = .map method in js
            var ret =
                stations.Select(async s =>
                {
                    return new StationOverviewDto()
                    {
                        //if no flight in station return null else get the flight
                        FlightInStation = s.FlightId != null ? await controlCenter.Flights.Get((int)s.FlightId) : null,
                        IsAvailable = s.FlightId == null,
                        StationId = s.StationId,
                        Name = s.Name
                    };
                }).Select(t => t.Result);      //since we are getting a task (using async/await) we have to get each result

            return Ok(ret);
        }

        /// <summary>
        /// Get the history (flights who come and go) of a station
        /// </summary>
        /// <param name="stationId">The specified id of a station</param>
        /// <returns>Collection of StationHistoryDto</returns>
        [HttpGet("get-station-history/{stationId:int}")]
        public async Task<ActionResult<IEnumerable<StationHistoryDto>>?> GetStationHistory(int stationId)
        {
            var station = await controlCenter.Stations.Get(stationId);
            var afs = await controlCenter.ArrivingFlights.GetHistoryByStationId(stationId);
            var dfs = await controlCenter.DepartingFlights.GetHistoryByStationId(stationId);

            // 1. Make a query that join arriving and departing flights on FlightId
            // 2. Map query results into StationHistoryDto
            var ret =
                from af in afs
                join df in dfs
                on af.FlightId equals df.FlightId
                select new StationHistoryDto()
                {
                    ArrivedAt = af.ArrivedAt,
                    DepartedAt = df.DepartedAt,
                    FlightId = df.FlightId
                };

            return Ok(ret);
        }

        [HttpGet("scheduled-flights")]
        public async Task<ActionResult<IEnumerable<ScheduledFlightDto>>?> GetScheduledFlights()
        {
            var dfs = await controlCenter.DepartingFlights.GetPending();    //gets flights that haven't left yet

            if (dfs == null) return NoContent();

            var ret = dfs
                .Select(df => new ScheduledFlightDto()
                {
                    FlightId = df.FlightId,
                    Flight = controlCenter.Flights.Get(df.FlightId).Result,
                    From = df.StationId,
                    To = df.StationId + 1,
                    Status = $"Pending at station {df.StationId}"
                })
                .OrderBy(s => s.FlightId);

            return Ok(ret);
        }

        [HttpGet("flights-history")]
        public async Task<ActionResult<IEnumerable<FlightHistoryDto>>?> GetFlightsHistory()
        {
            var flights = await controlCenter.Flights.GetAll();
            var stations = await controlCenter.Stations.GetAll();
            var afs = await controlCenter.ArrivingFlights.GetAll();
            var dfs = await controlCenter.DepartingFlights.GetAll();

            #region SQL query
            //  select Code, D.StationId as 'FROM', A.StationId as 'TO', ArrivedAt
            //  from Flights F, ArrivingFlights A, DepartingFlights D, Stations S
            //
            //where
            //    F.FlightId = A.FlightId
            //  and F.FlightId = D.FlightId
            //  and A.FlightId = D.FlightId
            //  and A.StationId = S.StationId
            //  and D.StationId = S.StationId - 1
            //  order by Code
            #endregion

            var ret =
                (
                from f in flights
                join af in afs on f.FlightId equals af.FlightId
                join df in dfs on f.FlightId equals df.FlightId
                join s in stations on af.StationId equals s.StationId
                where f.FlightId == af.FlightId
                where f.FlightId == df.FlightId
                where af.FlightId == df.FlightId
                where af.StationId == s.StationId
                where df.StationId == s.StationId - 1
                select new FlightHistoryDto()
                {
                    FlightId = f.FlightId,
                    Code = f.Code,
                    From = df.StationId,
                    To = af.StationId,
                    ArrivedAt = af.ArrivedAt
                }
                ).OrderBy(x => x.Code);

            return Ok(ret);
        }
    }
}
