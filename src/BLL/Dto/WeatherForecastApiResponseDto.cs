using System.Collections.Generic;

namespace BLL.Dto
{
    internal class WeatherForecastApiResponseDto
    {
        public CityName City { get; set; }

        public DailyTempInfo[] List { get; set; }
    }

    internal class CityName
    {
        public string Name { get; set; }
    }

    internal class DailyTempInfo
    {
        public DailyTemp Temp { get; set; }
    }

    internal class DailyTemp
    {
        public double Day { get; set; }
    }
}
