using back_end_api.Repository.Models;

namespace back_end_api.Dtos.Logic
{
    public class StationOverviewDto
    {
        public int StationId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public Flight? FlightInStation { get; set; }
        //public int? DelayInSeconds { get; set; }
    }
}
