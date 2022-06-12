using BLL.Dto.Options;

namespace BLL.Interfaces
{
    public interface IWeatherHistorySaver
    {
        void ManageHangfireJobs(WeatherHistoryOptions inputData);
    }
}
