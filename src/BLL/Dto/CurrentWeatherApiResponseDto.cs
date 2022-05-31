namespace BLL.Dto
{
    internal class CurrentWeatherApiResponseDto
    {
        public string Name { get; set; }

        public TempInfo Main { get; set; }

        public Coordinates Coord { get; set; }
    }

    internal class TempInfo
    {
        public double Temp { get; set; }
    }

    internal class Coordinates
    {
        public double Lat { get; set; }

        public double Lon { get; set; }
    }
}
