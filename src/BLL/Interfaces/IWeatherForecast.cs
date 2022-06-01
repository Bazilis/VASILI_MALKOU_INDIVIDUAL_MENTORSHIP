using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IWeatherForecast
    {
        string GetWeatherForecast(WeatherForecastInputDataDto inputData);
    }
}
