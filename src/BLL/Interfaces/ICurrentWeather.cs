using BLL.Dto;

namespace BLL.Interfaces
{
    public interface ICurrentWeather
    {
        string GetCurrentWeather(CurrentWeatherInputDataDto inputData);
    }
}
