using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace back_end_api.Repository.Models
{
    public partial class Flight
    {
        //public Flight()
        //{
        //    ArrivingFlights = new HashSet<ArrivingFlight>();
        //    DepartingFlights = new HashSet<DepartingFlight>();
        //}

        [Key]
        public int FlightId { get; set; }
        [StringLength(8)]
        public string Code { get; set; } = null!; //EDS775
        [StringLength(20)]
        public string Airline { get; set; } = null!;
        public int PrepTime { get; set; }
        [ForeignKey("StationId")]
        public int? StationId { get; set; }

        //[InverseProperty("Flight")]
        //public virtual ICollection<ArrivingFlight> ArrivingFlights { get; set; }
        //[InverseProperty("Flight")]
        //public virtual ICollection<DepartingFlight> DepartingFlights { get; set; }
    }
}
