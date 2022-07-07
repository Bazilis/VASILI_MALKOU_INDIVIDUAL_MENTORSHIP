namespace BLL.Dto
{
    internal class WeatherStatisticalReportApiResponseDto
    {
        public HourlyTempInfo[] List { get; set; }
    }

    internal class HourlyTempInfo
    {
        public HourlyTemp Main { get; set; }
    }

    internal class HourlyTemp
    {
        public double Temp { get; set; }
    }
}
