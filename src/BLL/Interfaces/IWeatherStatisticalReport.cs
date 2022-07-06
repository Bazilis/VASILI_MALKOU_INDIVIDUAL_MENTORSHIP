using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IWeatherStatisticalReport
    {
        string GetWeatherStatisticalReport(string citiesString, TimePeriodValues timePeriod);
    }
}
