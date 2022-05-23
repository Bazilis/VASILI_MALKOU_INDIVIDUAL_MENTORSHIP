namespace BLL.Dto
{
    internal class CurrentWeatherApiResponseDto
    {
        public string Name { get; set; }

        public TempInfo Main { get; set; }
    }

    internal class TempInfo
    {
        public double Temp { get; set; }
    }
}
