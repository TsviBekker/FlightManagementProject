using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace back_end_api.Repository.Models
{
    public partial class Station
    {
        //public Station()
        //{
        //    ArrivingFlights = new HashSet<ArrivingFlight>();
        //    DepartingFlights = new HashSet<DepartingFlight>();
        //}

        [Key]
        public int StationId { get; set; }
        [StringLength(20)]
        public string Name { get; set; } = null!;
        //public bool IsAvailable { get; set; }
        [ForeignKey("FlightId")]
        public int? FlightId { get; set; }

        //[InverseProperty("Station")]
        //public virtual ICollection<ArrivingFlight> ArrivingFlights { get; set; }
        //[InverseProperty("Station")]
        //public virtual ICollection<DepartingFlight> DepartingFlights { get; set; }
    }
}
