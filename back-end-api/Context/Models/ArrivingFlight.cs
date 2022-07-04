using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace back_end_api.Repository.Models
{
    public partial class ArrivingFlight
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FlightId")]
        public int FlightId { get; set; }
        [ForeignKey("StationId")]
        public int StationId { get; set; }
        public bool HasArrived { get; set; }

        [ForeignKey("FlightId")]
        [InverseProperty("ArrivingFlights")]
        public virtual Flight Flight { get; set; } = null!;
        [ForeignKey("StationId")]
        [InverseProperty("ArrivingFlights")]
        public virtual Station Station { get; set; } = null!;
    }
}
