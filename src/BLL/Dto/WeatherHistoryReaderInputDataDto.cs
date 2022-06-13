using System;

namespace BLL.Dto
{
    public class WeatherHistoryReaderInputDataDto
    {
        public string CityName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
