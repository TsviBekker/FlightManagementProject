using back_end_api.Repository.Models;

namespace back_end_api.Dtos.Logic
{
    public class StationHistoryDto
    {
        public int FlightId { get; set; }
        public DateTime? ArrivedAt { get; set; }
        public DateTime? DepartedAt { get; set; }
    }
}
