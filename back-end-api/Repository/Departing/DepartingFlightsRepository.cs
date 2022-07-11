﻿using back_end_api.Context;
using back_end_api.Repository.Generic;
using back_end_api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end_api.Repository.Departing
{
    public class DepartingFlightsRepository : GenericRepository<DepartingFlight>, IDepartingFlightsRepository
    {
        public DepartingFlightsRepository(FlightsDbContext context) : base(context)
        {
        }
        public async Task<DepartingFlight?> GetByStationAndFlight(int stationId, int flightId)
        {
            return await context.DepartingFlights.FirstOrDefaultAsync(f => f.FlightId == flightId && f.StationId == stationId);
        }
    }
}
