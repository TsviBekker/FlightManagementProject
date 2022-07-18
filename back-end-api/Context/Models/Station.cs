using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_api.Repository.Models
{
    public partial class Station
    {
        [Key]
        public int StationId { get; set; }
        [StringLength(20)]
        public string Name { get; set; } = null!;

        [ForeignKey("FlightId")]
        public int? FlightId { get; set; }
    }
}
