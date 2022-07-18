using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_api.Repository.Models
{
    public partial class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [StringLength(8)]
        public string Code { get; set; } = null!; //e.g EDS775

        [StringLength(20)]
        public string Airline { get; set; } = null!;
        public int PrepTime { get; set; }

        [ForeignKey("StationId")]
        public int? StationId { get; set; }
    }
}
