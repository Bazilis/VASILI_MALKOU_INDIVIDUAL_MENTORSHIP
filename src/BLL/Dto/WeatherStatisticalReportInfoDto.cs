namespace BLL.Dto
{
    public class WeatherStatisticalReportInfoDto
    {
        public string CityName { get; set; }

        public double AverageCityTemp { get; set; }

        public bool ResponseState { get; set; }

        public string ErrorMessage { get; set; }
    }
}
