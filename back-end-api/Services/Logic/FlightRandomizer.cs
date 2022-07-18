using back_end_api.ControlCenter;

namespace back_end_api.Services.Logic
{
    public static class FlightRandomizer
    {
        private static readonly Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string GenerateCode()
        {
            int charCount = random.Next(1, 4);
            int num = random.Next(1, 1000);
            string str = new string(Enumerable.Repeat(chars, charCount).Select(s => s[random.Next(chars.Length)]).ToArray());
            return $"{str}{num}";
        }

        public static int GeneratePrepTime()
        {
            return random.Next(5, 15);
        }

        public static int GenerateNextStation(IControlCenter controlCenter)
        {
            if (random.Next(0, 5) == 0) return 9;
            var stations = controlCenter.Stations.GetAvailable().Where(s => s.StationId != 1).ToList();
            return stations[random.Next(0, stations.Count - 1)].StationId;
        }
    }
}
