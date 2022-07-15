using BLL.Dto;
using BLL.Interfaces;
using Hangfire;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BLL.Services
{
    public class SubscribeUserService : ISubscribeUser
    {
        private readonly IEmailSender _emailSender;
        private readonly IWeatherStatisticalReport _statisticalReport;

        public SubscribeUserService(IEmailSender emailSender, IWeatherStatisticalReport statisticalReport)
        {
            _emailSender = emailSender;
            _statisticalReport = statisticalReport;
        }

        public string SubscribeUserByUserId(WeatherStatisticalReportInputDataDto inputData)
        {
            var emailSubject = $"Statistical report at {DateTime.Now}";

            var statisticalReportString = _statisticalReport.GetWeatherStatisticalReport(inputData.CitiesString, inputData.TimePeriod);

            var result = _emailSender.SendEmail(GetUserEmail(inputData.UserGuid), emailSubject, statisticalReportString, inputData.IsUseRabbitmq);

            if (!result.Item1)
                return result.Item2;

            var timePeriod = (int)inputData.TimePeriod switch
            {
                1 => "0 * * * *",
                3 => "0 0 */3 ? * *",
                12 => "0 0 */12 ? * *",
                24 => "0 0 * * *",
                _ => throw new ArgumentException(nameof(inputData.TimePeriod)),
            };

            RecurringJob.AddOrUpdate($"Subscribe user job", () => SubscribeUserJob(inputData), $"{timePeriod}");

            return $"User with UserId {inputData.UserGuid} subscribed to weather forecast statistical email reports every {inputData.TimePeriod}";
        }

        private void SubscribeUserJob(WeatherStatisticalReportInputDataDto inputData)
        {
            var emailSubject = $"Statistical report at {DateTime.Now}";

            var statisticalReportString = _statisticalReport.GetWeatherStatisticalReport(inputData.CitiesString, inputData.TimePeriod);

            _ = _emailSender.SendEmail(GetUserEmail(inputData.UserGuid), emailSubject, statisticalReportString, inputData.IsUseRabbitmq);
        }

        private string GetUserEmail(string userGuid)
        {
            var url = $"https://localhost:7299/api/user/{userGuid}";

            var request = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    string responseStr;

                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseStr = reader.ReadToEnd();
                    }

                    return JsonConvert.DeserializeObject<string>(responseStr);
                }
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
        }
    }
}
