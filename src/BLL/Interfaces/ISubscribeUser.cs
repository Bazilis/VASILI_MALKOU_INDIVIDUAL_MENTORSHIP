using BLL.Dto;

namespace BLL.Interfaces
{
    public interface ISubscribeUser
    {
        string SubscribeUserByUserId(WeatherStatisticalReportInputDataDto inputData, bool isUseRabbitmq);
    }
}
