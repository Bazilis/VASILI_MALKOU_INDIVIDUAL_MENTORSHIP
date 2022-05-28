using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IWeatherForecast
    {
        public string GetWeatherForecast(WeatherForecastInputDataDto inputData);
    }
}
