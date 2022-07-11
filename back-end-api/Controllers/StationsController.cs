using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back_end_api.Repository;
using back_end_api.Repository.Models;
using back_end_api.Context;
using back_end_api.Dtos.Logic;

namespace back_end_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly FlightsDbContext _context;

        public StationsController(FlightsDbContext context)
        {
            _context = context;
        }

        ////GET: api/StationsOverview
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<StationOverviewDto>>> GetStationsOverview()
        //{
        //    if (_context.Stations == null)
        //    {
        //        return NotFound();
        //    }
        //    var lst = new List<StationOverviewDto>();
        //    foreach (var item in _context.Stations)
        //    {
                
        //    }
        //}

        // GET: api/Stations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Station>>> GetStations()
        {
          if (_context.Stations == null)
          {
              return NotFound();
          }
            return await _context.Stations.ToListAsync();
        }

        // GET: api/Stations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Station>> GetStation(int id)
        {
          if (_context.Stations == null)
          {
              return NotFound();
          }
            var station = await _context.Stations.FindAsync(id);

            if (station == null)
            {
                return NotFound();
            }

            return station;
        }

        // PUT: api/Stations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStation(int id, Station station)
        {
            if (id != station.StationId)
            {
                return BadRequest();
            }

            _context.Entry(station).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Stations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Station>> PostStation(Station station)
        {
          if (_context.Stations == null)
          {
              return Problem("Entity set 'FlightsDbContext.Stations'  is null.");
          }
            _context.Stations.Add(station);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStation", new { id = station.StationId }, station);
        }

        // DELETE: api/Stations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(int id)
        {
            if (_context.Stations == null)
            {
                return NotFound();
            }
            var station = await _context.Stations.FindAsync(id);
            if (station == null)
            {
                return NotFound();
            }

            _context.Stations.Remove(station);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool StationExists(int id)
        {
            return (_context.Stations?.Any(e => e.StationId == id)).GetValueOrDefault();
        }
    }
}
