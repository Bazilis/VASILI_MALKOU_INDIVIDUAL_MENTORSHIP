using BLL.Dto;

namespace BLL.Interfaces
{
    public interface ICurrentWeather
    {
        public string GetCurrentWeather(CurrentWeatherInputDataDto inputData);
    }
}
