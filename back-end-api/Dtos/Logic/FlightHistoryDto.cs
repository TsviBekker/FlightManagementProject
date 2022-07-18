namespace back_end_api.Dtos.Logic
{
    public class FlightHistoryDto
    {
        public int FlightId { get; set; }
        public string? Code { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public DateTime? ArrivedAt { get; set; }
    }
}
