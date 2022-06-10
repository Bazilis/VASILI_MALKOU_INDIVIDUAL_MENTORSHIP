using BLL.Dto.Options;

namespace BLL.Interfaces
{
    public interface IWeatherHistory
    {
        void ManageHangfireJobs(WeatherHistoryOptions inputData);
    }
}
