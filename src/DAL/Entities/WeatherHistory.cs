using System;

namespace DAL.Entities
{
    public class WeatherHistory
    {
        public int Id { get; set; }

        public string CityName { get; set; }

        public double CityTemp { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime DateTime { get; set; }
    }
}
