using back_end_api.Repository.Models;

namespace back_end_api.Dtos.Logic
{
    public class ScheduledFlightDto
    {
        public int FlightId { get; set; }
        public Flight? Flight { get; set; }
        public string? Status { get; set; }
        public int From { get; set; }
        public int To { get; set; }

    }
}
