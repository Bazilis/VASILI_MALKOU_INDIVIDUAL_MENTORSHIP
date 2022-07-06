using BLL.Dto;
using BLL.Interfaces;
using Hangfire;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BLL.Services
{
    public class SubscribeUserService
    {
        private readonly IWeatherStatisticalReport _statisticalReport;

        public SubscribeUserService(IWeatherStatisticalReport statisticalReport)
        {
            _statisticalReport = statisticalReport;
        }

        public void SubscribeUserByUserId(WeatherStatisticalReportInputDataDto inputData)
        {
            var timePeriod = (int)inputData.TimePeriod switch
            {
                1 => "0 * * * *",
                3 => "0 0 */3 ? * *",
                12 => "0 0 */12 ? * *",
                24 => "0 0 * * *",
                _ => throw new ArgumentException(nameof(inputData.TimePeriod)),
            };

            RecurringJob.AddOrUpdate($"Subscribe user job", () => SubscribeUserJob(inputData), $"{timePeriod}");
        }

        private void SubscribeUserJob(WeatherStatisticalReportInputDataDto inputData)
        {
            var statisticalReportString = _statisticalReport.GetWeatherStatisticalReport(inputData.CitiesString, inputData.TimePeriod);

            //SendEmail(GetUserEmail(inputData.UserGuid), statisticalReportString);
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
