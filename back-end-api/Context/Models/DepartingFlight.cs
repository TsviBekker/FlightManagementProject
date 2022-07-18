using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_api.Repository.Models
{
    public partial class DepartingFlight
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("FlightId")]
        public int FlightId { get; set; }

        [ForeignKey("StationId")]
        public int StationId { get; set; }
        public bool HasDeparted { get; set; }
        public DateTime? DepartedAt { get; set; }
    }
}
