using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IWeatherHistoryReader
    {
        WeatherHistoryDto[] GetWeatherHistoryData(WeatherHistoryReaderInputDataDto inputData);
    }
}
