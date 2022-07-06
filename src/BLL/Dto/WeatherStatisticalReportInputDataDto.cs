namespace BLL.Dto
{
    public class WeatherStatisticalReportInputDataDto
    {
        public string UserGuid { get; set; }

        public string CitiesString { get; set; }

        public TimePeriodValues TimePeriod { get; set; }
    }

    public enum TimePeriodValues
    {
        OneHour = 1,
        ThreeHours = 3,
        TwelveHours = 12,
        OneDay = 24
    }
}
