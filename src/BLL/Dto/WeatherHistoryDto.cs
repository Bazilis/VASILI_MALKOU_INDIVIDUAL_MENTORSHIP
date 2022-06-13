using System;

namespace BLL.Dto
{
    public class WeatherHistoryDto
    {
        public int Id { get; set; }

        public string CityName { get; set; }

        public double CityTemp { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime DateTime { get; set; }
    }
}
